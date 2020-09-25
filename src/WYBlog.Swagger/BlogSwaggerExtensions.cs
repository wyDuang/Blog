using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
        private static readonly string description = @"<p><b>Blog</b>：<a target=""_blank"" href=""https://wyduang.com"">https://wyduang.com</a></p>
                <p><b>GitHub</b>：<a target=""_blank"" href=""https://github.com/wyDuang/Blog"">https://github.com/wyDuang/Blog</a></p>
                <p><b>Hangfire</b>：<a target=""_blank"" href=""/hangfire"">任务调度中心</a></p>
                <p><a href=""mailto:110@wyduang.com"" rel=""noopener noreferrer"" class=""link"" > Send email to 110@wyduang.com</a></p>
                <p>Powered by .NET Core 3.1 on Linux</p>";

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
            }
        };

        /// <summary>
        /// AddSwagger
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static void AddSwagger(this IServiceCollection services)
        {
            if (null == services) throw new ArgumentNullException(nameof(services));

            services.AddSwaggerGen(options => {
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
                    Name = "Authorization", //jwt默认的参数名称
                    In = ParameterLocation.Header, //jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey
                };
                options.AddSecurityDefinition("oauth2", security);
                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    { security, new List<string>() }
                });

                options.OperationFilter<AddResponseHeadersFilter>();
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                options.OperationFilter<SecurityRequirementsOperationFilter>();

                #endregion

                // 应用Controller的API文档描述信息
                options.DocumentFilter<SwaggerDocumentFilter>();
            });
        }


        /// <summary>
        /// UseCustomSwaggerUI
        /// </summary>
        /// <param name="app"></param>
        public static void UseSwaggerUI(this IApplicationBuilder app)
        {
            app.UseSwaggerUI(options =>
            {
                // 遍历分组信息，生成Json
                ApiInfos.ForEach(x =>
                {
                    options.SwaggerEndpoint($"/swagger/{x.UrlPrefix}/swagger.json", x.Name);
                });

                options.DefaultModelsExpandDepth(-1);//模型的默认扩展深度，设置为 - 1 完全隐藏模型                
                options.DocExpansion(DocExpansion.List);//API文档仅展开标记
                options.RoutePrefix = string.Empty;//API前缀设置为空
                options.DocumentTitle = "😎 wyDuang 接口文档 | api.wyduang.com";//API页面Title
            });
        }
    }
}