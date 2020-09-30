using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Volo.Abp.Validation;
using WYBlog.Extensions;

namespace WYBlog.Middleware
{
    /// <summary>
    /// 自定义异常处理中间件
    /// </summary>
    public class BlogExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<BlogExceptionHandlerMiddleware> _logger;

        public BlogExceptionHandlerMiddleware(RequestDelegate next, ILogger<BlogExceptionHandlerMiddleware> logger)
        {
            this.next = next;
            _logger  = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                if (ex is AbpValidationException)
                {
                    var validationErrors = ((AbpValidationException)ex).ValidationErrors;
                    var message = ex.Message;
                    if (validationErrors?.Count > 0)
                    {
                        message = string.Join(" ", validationErrors.Select(x => x.ErrorMessage));
                    }

                    context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                    await ExceptionHandlerAsync(ex.HResult, ex, context, message);
                }
                else
                {
                    await ExceptionHandlerAsync(ex.HResult, ex, context, ex.Message);
                }
            }
        }

        /// <summary>
        /// 异常处理，记录异常的日志、返回异常提示JSON
        /// </summary>
        /// <returns></returns>
        private async Task ExceptionHandlerAsync(int hResult, Exception ex, HttpContext context, string message)
        {
            _logger.LogError(new EventId(hResult), ex, $"请求地址：{context.Request.Path}\r\n{message}\r\nStatusCode：{context.Response.StatusCode}，Ip：{context.GetClientUserIp()}，User-Agent：{context.Request.Headers["User-Agent"]}");

            context.Response.ContentType = "application/json;charset=utf-8";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new { code = context.Response.StatusCode, msg = message }));            
        }
    }
}