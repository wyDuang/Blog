using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using WYBlog.Configurations;

namespace WYBlog
{
    public static class BlogSwaggerExtensions
    {
        /// <summary>
        /// 当前API版本，从appsettings.json获取
        /// </summary>
        private static readonly string version = $"v{AppSettings.ApiVersion}";

        /// <summary>
        /// Swagger描述信息
        /// </summary>
        private static readonly string description = @"<b>Blog</b>：<a target=""_blank"" href=""https://wyduang.com"">https://wyduang.com</a> <b>GitHub</b>：<a target=""_blank"" href=""https://github.com/Meowv/Blog"">https://github.com/wyDuang/Blog</a> <b>Hangfire</b>：<a target=""_blank"" href=""/hangfire"">任务调度中心</a> <code>Powered by .NET Core 3.1 on Linux</code>";

        /// <summary>
        /// Swagger分组信息，将进行遍历使用
        /// </summary>
        private static readonly List<SwaggerApiInfo> ApiInfos = new List<SwaggerApiInfo>()
        {
            new SwaggerApiInfo
            {
                UrlPrefix = ApiGroupingConsts.GroupName_v1,
                Name = "博客数据接口",
                OpenApiInfo = new OpenApiInfo
                {
                    Version = version,
                    Title = "wyDuang - 博客数据接口",
                    Description = description
                }
            },
            new SwaggerApiInfo
            {
                UrlPrefix = ApiGroupingConsts.GroupName_v2,
                Name = "通用数据接口",
                OpenApiInfo = new OpenApiInfo
                {
                    Version = version,
                    Title = "wyDuang - 通用数据接口",
                    Description = description
                }
            },
            new SwaggerApiInfo
            {
                UrlPrefix = ApiGroupingConsts.GroupName_v3,
                Name = "JWT授权接口",
                OpenApiInfo = new OpenApiInfo
                {
                    Version = version,
                    Title = "wyDuang - JWT授权接口",
                    Description = description
                }
            }
        };

        public static IServiceCollection AddSwaggerGen(this IServiceCollection services)
        {
            return services.AddSwaggerGen(options => {
                // 遍历并应用Swagger分组信息
                ApiInfos.ForEach(x =>
                {
                    options.SwaggerDoc(x.UrlPrefix, x.OpenApiInfo);
                });

                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Resources/WYBlog.HttpApi.xml"));
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Resources/WYBlog.Domain.xml"));
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Resources/WYBlog.Application.Contracts.xml"));

                #region 小绿锁，JWT身份认证配置

                var security = new OpenApiSecurityScheme
                {
                    Description = "JWT模式授权，请输入 Bearer {Token} 进行身份验证",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                };
                options.AddSecurityDefinition("oauth2", security);
                options.AddSecurityRequirement(new OpenApiSecurityRequirement { { security, new List<string>() } });
                options.OperationFilter<AddResponseHeadersFilter>();
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                options.OperationFilter<SecurityRequirementsOperationFilter>();

                #endregion

                // 应用Controller的API文档描述信息
                options.DocumentFilter<SwaggerDocumentFilter>();
            });
        }
    }
}