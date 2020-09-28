using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Linq;
using Volo.Abp.Validation;

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

            if (context.Exception is AbpValidationException)
            {
                var validationErrors = ((AbpValidationException)context.Exception).ValidationErrors;
                var message = context.Exception.Message;
                if(validationErrors?.Count > 0)
                {
                    message = string.Join(" ", validationErrors.Select(x => x.ErrorMessage));
                }
                context.Result = new JsonResult(new { code = 100, msg = message });
            }
            else
            {
                context.Result = new JsonResult(new { code = 500, msg = "系统异常" });
            }
            //context.ExceptionHandled = true;//标记异常是否已处理
        }
    }
}