using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WYBlog.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        /// <summary>
        /// Invoke
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await context.Response.WriteAsync(ex.Message);
            }
            finally
            {
                var statusCode = context.Response.StatusCode;
                if (statusCode != StatusCodes.Status200OK)
                {
                    Enum.TryParse(typeof(HttpStatusCode), statusCode.ToString(), out object message);
                    await context.Response.WriteAsync(message.ToString());
                }
            }
        }

        ///// <summary>
        ///// 异常处理，返回JSON
        ///// </summary>
        ///// <param name="context"></param>
        ///// <param name="message"></param>
        ///// <returns></returns>
        //private async Task ExceptionHandlerAsync(HttpContext context, string message)
        //{
        //    context.Response.ContentType = "application/json;charset=utf-8";

        //    var result = new ServiceResult();
        //    result.IsFailed(message);

        //    await context.Response.WriteAsync(result.ToJson());
        //}
    }
}
