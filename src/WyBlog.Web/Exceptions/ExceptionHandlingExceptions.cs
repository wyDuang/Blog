using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WyBlog.Web.Exceptions
{
    /// <summary>
    /// 自定义全局异常处理类，返回json
    /// </summary>
    public static class ExceptionHandlingExceptions
    {
        /// <summary>
        /// 注册自定义全局异常处理类
        /// </summary>
        /// <param name="app"></param>
        /// <param name="loggerFactory"></param>
        public static void UseMyExceptionHandler(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";

                    //获取异常
                    var ex = context.Features.Get<IExceptionHandlerFeature>();
                    if (ex != null)
                    {
                        //异常不为空时记录日志。
                        var logger = loggerFactory.CreateLogger("WyBlog.Api.Exceptions.ExceptionHandlingExtensions");
                        logger.LogError(500, ex.Error, ex.Error.Message);
                    }

                    await context.Response.WriteAsync(ex?.Error.Message ?? "发生错误!");
                });
            });
        }
    }
}
