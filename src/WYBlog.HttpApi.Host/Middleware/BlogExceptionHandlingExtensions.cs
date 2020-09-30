using Microsoft.AspNetCore.Builder;
using System;

namespace WYBlog.Middleware
{
    public static class BlogExceptionHandlingExtensions
    {
        public static IApplicationBuilder UseBlogExceptionHandling(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseMiddleware<BlogExceptionHandlerMiddleware>();
        }
    }
}