using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.ResultModel
{
    /// <summary>
    /// 自定义 无法解析的实体对象 返回结果
    /// </summary>
    public class MyUnprocessableEntityObjectResult : UnprocessableEntityObjectResult
    {
        public MyUnprocessableEntityObjectResult(ModelStateDictionary modelState)
            : base(new ResourceValidationResult(modelState))
        {
            if(modelState == null)
            {
                throw new ArgumentNullException(nameof(modelState));
            }
            StatusCode = 422;//表示请求的格式没问题，但是语义有错误，例如实体验证错误
        }
    }
}
