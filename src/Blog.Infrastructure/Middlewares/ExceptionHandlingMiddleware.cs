using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Blog.Infrastructure.Middlewares
{
    public static class ExceptionHandlingMiddleware
    {
        public static void UseMyExceptionHandler(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseExceptionHandler(builder =>
            {
                builder.UseCors("BlogApiCors");
                builder.Run(
                    async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "application/json";
                        var ex = context.Features.Get<IExceptionHandlerFeature>();
                        if (ex != null)
                        {
                            var logger = loggerFactory.CreateLogger("全局异常记录器");
                            logger.LogError(500, ex.Error, ex.Error.Message);
                        }
                        await context.Response.WriteAsync(ex?.Error?.Message ?? "发生错误。");
                    });
            });
        }
    }
}
