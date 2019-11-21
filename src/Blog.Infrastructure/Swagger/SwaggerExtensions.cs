using Blog.Infrastructure.Swagger;
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

namespace Blog.Infrastructure.Swagger
{
    public static class SwaggerExtensions
    {
        public static List<SwaggerApiInfo> ApiInfos = new List<SwaggerApiInfo>()
        {
            new SwaggerApiInfo
            {
                Version = ApiVersionConsts.GroupName_v1,
                Name = "博客数据接口",
                OpenApiInfo = new OpenApiInfo
                {
                    Version = "v1.0",
                    Title = "博客数据接口 - 接口文档",
                    Description = $"wyDuang - .Net Core 3.0 - RESTful API ",
                    Contact = new OpenApiContact { Url = new Uri("https://wyduang.com"), Name = "wyDuang", Email = "110@wyduang.com" }
                }
            },
            new SwaggerApiInfo
            {
                Version = ApiVersionConsts.GroupName_v2,
                Name = "其他数据接口",
                OpenApiInfo = new OpenApiInfo
                {
                    Version = "v1.0",
                    Title = "其他数据接口 - 接口文档",
                    Description = $"wyDuang - .Net Core 3.0 - RESTful API ",
                    Contact = new OpenApiContact { 
                        Name = "wyDuang", 
                        Email = "110@wyduang.com", 
                        Url = new Uri("https://wyduang.com") 
                    },
                    TermsOfService = new Uri("https://wyduang.com")
                }
            },
        };

        public static void AddSwagger(this IServiceCollection services)
        {
            if (null == services) throw new ArgumentNullException(nameof(services));

            services.AddSwaggerGen(options =>
            {
                ApiInfos.ForEach(x =>
                {
                    options.SwaggerDoc(x.Version, x.OpenApiInfo);
                    options.OrderActionsBy(o => o.RelativePath);
                });

                ////设置要展示的接口
                //options.DocInclusionPredicate((docName, apiDes) =>
                //{
                //    if (!apiDes.TryGetMethodInfo(out MethodInfo method)) return false;

                //    /* 使用ApiExplorerSettingsAttribute里面的GroupName进行特性标识
                //     * DeclaringType只能获取controller上的特性
                //     * 我们这里是想以action的特性为主
                //     */

                //    var version = method.DeclaringType.GetCustomAttributes(true).OfType<ApiExplorerSettingsAttribute>().Select(m => m.GroupName);

                //    if (docName == "v1" && !version.Any()) return true;

                //    //这里获取action的特性
                //    var actionVersion = method.GetCustomAttributes(true).OfType<ApiExplorerSettingsAttribute>().Select(m => m.GroupName);
                    
                //    if (actionVersion.Any())
                //        return actionVersion.Any(v => v == docName);

                //    return version.Any(v => v == docName);
                //});

                var security = new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输)直接在下框中输入Bearer[空格]{Token}",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey
                };
                options.AddSecurityDefinition("oauth2", security); // Token绑定到ConfigureServices
                options.AddSecurityRequirement(
                    new OpenApiSecurityRequirement {
                        { security, Array.Empty<string>() }
                });

                options.DocumentFilter<SwaggerDocumentFilter>();
                options.OperationFilter<SwaggerAuthTokenHeaderFilter>();
                options.OperationFilter<AddResponseHeadersFilter>();
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                options.OperationFilter<SecurityRequirementsOperationFilter>();

                try
                {
                    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Blog.Api.xml"));
                    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Blog.Core.xml"));
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
            });
        }

        public static void UseSwaggerUI(this IApplicationBuilder app)
        {
            app.UseSwaggerUI(options =>
            {
                ApiInfos.ForEach(x =>
                {
                    options.SwaggerEndpoint($"/swagger/{x.Version}/swagger.json", x.Name);
                });
                options.DefaultModelsExpandDepth(-1);
                options.DocExpansion(DocExpansion.List);
                options.RoutePrefix = string.Empty;
                options.DocumentTitle = "😎 wyDuang 接口文档 | api.wyduang.com";
            });
        }
    }
}
