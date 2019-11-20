using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Blog.Infrastructure.Swagger
{
    /// <summary>
    /// 控制swagger中是否需要添加accesstoken验证
    /// </summary>
    public class SwaggerAuthTokenHeaderFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }

            var attrs = context.ApiDescription.ActionDescriptor.AttributeRouteInfo;

            //先判断是否是匿名访问
            var descriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;
            if (descriptor != null)
            {
                var actionAttributes = descriptor.MethodInfo.GetCustomAttributes(inherit: true);
                var isAnonymous = actionAttributes.Any(a => a is AllowAnonymousAttribute);
                var isAuthorized = actionAttributes.Any(a => a is AuthorizeAttribute);

                //非匿名的方法,链接中添加accesstoken值
                if (!isAnonymous && isAuthorized)
                {
                    operation.Parameters.Add(new OpenApiParameter()
                    {
                        Name = "Authorization",
                        In = ParameterLocation.Header,//查询头正文路径formData
                        Description = "JWT授权(数据将在请求头中进行传输)直接在下框中输入Bearer[空格]{Token}",
                        Schema = new OpenApiSchema()
                        {
                            Type = "string",
                        },
                        Required = true //是否必选
                    });
                }
            }
        }
    }
}
