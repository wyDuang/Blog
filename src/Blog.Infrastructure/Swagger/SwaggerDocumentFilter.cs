﻿using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Infrastructure.Swagger
{
    public class SwaggerDocumentFilter: IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var tags = new List<OpenApiTag>
            {
                new OpenApiTag {
                    Name = "Auth",
                    Description = "JWT模式认证授权",
                    ExternalDocs = new OpenApiExternalDocs { Description = "JSON Web Token" }
                },
                new OpenApiTag {
                    Name = "Blog",
                    Description = "博客数据接口",
                    ExternalDocs = new OpenApiExternalDocs { Description = "分类/文章/标签/友链" }
                },
                new OpenApiTag {
                    Name = "Common",
                    Description = "通用数据接口",
                    ExternalDocs = new OpenApiExternalDocs { Description = "Common 未开发..." }
                },
                //new OpenApiTag {
                //    Name = "MTA",
                //    Description = "腾讯移动分析",
                //    ExternalDocs = new OpenApiExternalDocs { Description = "MTA 未开发..." }
                //},
                //new OpenApiTag {
                //    Name = "Signature",
                //    Description = "个性艺术签名设计",
                //    ExternalDocs = new OpenApiExternalDocs { Description = "Signature" }
                //}
            };

            swaggerDoc.Tags = tags.OrderBy(x => x.Name).ToList();
        }
    }
}