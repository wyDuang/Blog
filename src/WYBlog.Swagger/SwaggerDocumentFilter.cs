using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WYBlog
{
    public class SwaggerDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var tags = new List<OpenApiTag>
            {
                new OpenApiTag {
                    Name = "Auth",
                    Description = "JWT模式认证授权接口",
                    ExternalDocs = new OpenApiExternalDocs { Description = "JSON Web Token" }
                },
                new OpenApiTag {
                    Name = "Common",
                    Description = "通用数据接口",
                    ExternalDocs = new OpenApiExternalDocs { Description = "Common 开发中..." }
                },
                new OpenApiTag {
                    Name = "Signature",
                    Description = "个性艺术签名设计接口",
                    ExternalDocs = new OpenApiExternalDocs { Description = "Signature 开发中..." }
                },
                new OpenApiTag {
                    Name = "Blog",
                    Description = "博客数据接口",
                    ExternalDocs = new OpenApiExternalDocs { Description = "分类/文章/标签/友链/留言" }
                }
            };

            #region 实现添加自定义描述时过滤不属于同一个分组的API

            // 当前分组名称
            var groupName = context.ApiDescriptions.Where(x => !string.IsNullOrEmpty(x.GroupName))?.FirstOrDefault().GroupName;

            // 当前所有的API对象
            var apis = context.ApiDescriptions
                .GetType()
                .GetField("_source", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(context.ApiDescriptions) as IEnumerable<ApiDescription>;

            // 属于当前分组的所有Controller 和 为GroupName=null的Controller
            // 注意：配置的OpenApiTag，Name名称要与Controller的Name对应才会生效。
            var controllers = apis
                .Where(x => x.GroupName == groupName || x.GroupName == null)
                .Select(x => ((ControllerActionDescriptor)x.ActionDescriptor).ControllerName)
                .Distinct();

            // 筛选一下tags
            swaggerDoc.Tags = tags.Where(x => controllers.Contains(x.Name))
                .OrderBy(x => x.Name)
                .ToList();

            #endregion
        }
    }
}