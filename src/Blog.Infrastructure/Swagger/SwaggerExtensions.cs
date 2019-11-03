using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Blog.Infrastructure.Swagger
{
    public static class SwaggerExtensions
    {
        private static readonly string version = "v1.0";

        public static List<SwaggerApiInfo> ApiInfos = new List<SwaggerApiInfo>()
        {
            new SwaggerApiInfo
            {
                UrlPrefix = GlobalConsts.GroupName_v1,
                Name = "博客数据接口",
                OpenApiInfo = new OpenApiInfo
                {
                    Version = version,
                    Title = "wyDuang - 博客数据接口"
                }
            },
            new SwaggerApiInfo
            {
                UrlPrefix = GlobalConsts.GroupName_v2,
                Name = "其他数据接口",
                OpenApiInfo = new OpenApiInfo
                {
                    Version = version,
                    Title = "wyDuang - 其他数据接口"
                }
            },
        };

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(options =>
            {
                ApiInfos.ForEach(x =>
                {
                    options.SwaggerDoc(x.UrlPrefix, x.OpenApiInfo);
                });

                //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "MeowvBlog.API.xml"));
                //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "MeowvBlog.Core.xml"));

                var security = new OpenApiSecurityScheme
                {
                    Description = "JWT模式授权，请输入 Bearer {Token} 进行身份验证",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                };
                options.AddSecurityDefinition("Bearer", security);

                security.Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                };
                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    { security, Array.Empty<string>() }
                });

                options.DocumentFilter<SwaggerDocumentFilter>();

                options.OperationFilter<AddResponseHeadersFilter>();
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });
        }

        public static void UseSwaggerUI(this IApplicationBuilder app)
        {
            app.UseSwaggerUI(options =>
            {
                ApiInfos.ForEach(x =>
                {
                    options.SwaggerEndpoint($"/swagger/{x.UrlPrefix}/swagger.json", x.Name);
                });
                options.DefaultModelsExpandDepth(-1);
                options.DocExpansion(DocExpansion.List);
                options.RoutePrefix = string.Empty;
                options.DocumentTitle = "😎 wyDuang 接口文档 | api.wyduang.com";
            });
        }
    }
}
