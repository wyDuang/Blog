using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WYBlog.Filters
{
    /// <summary>
    /// 全部异常处理过滤器
    /// </summary>
    public class BlogGlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<BlogGlobalExceptionFilter> _logger;
        public BlogGlobalExceptionFilter(ILogger<BlogGlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(new EventId(context.Exception.HResult), context.Exception, context.Exception.Message);

            //context.Result = new JsonResult(new { code = 500, err = "系统异常" });
            //context.ExceptionHandled = true;//标记异常是否已处理
        }
    }
}
