﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Text;
using Volo.Abp;
using Volo.Abp.AspNetCore.Authentication.JwtBearer;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.ExceptionHandling;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using WYBlog.Configurations;
using WYBlog.EntityFrameworkCore;
using WYBlog.Middleware;

namespace WYBlog
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpAspNetCoreMvcModule),
        typeof(AbpAspNetCoreAuthenticationJwtBearerModule),
        typeof(BlogSwaggerModule),
        typeof(BlogHttpApiModule),
        typeof(BlogApplicationModule),
        typeof(BlogBackgroundJobsModule),
        typeof(BlogEntityFrameworkCoreModule),
        typeof(BlogEntityFrameworkCoreDbMigrationsModule)
        )]
    public class BlogHttpApiHostModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            ConfigureExceptionFilter();
            ConfigureAutoApiControllers();
            ConfigureRouting(context.Services);
            ConfigureCors(context.Services);
            ConfigureAuthentication(context.Services);
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            app.UseHsts();

            // 转发将标头代理到当前请求，配合 Nginx 使用，获取用户真实IP
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseRouting();
            app.UseCors(BlogAppConsts.DefaultCorsPolicyName);
            app.UseBlogExceptionHandling();// 异常处理中间件
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        /// <summary>
        /// 异常过滤器
        /// </summary>
        private void ConfigureExceptionFilter()
        {
            Configure<MvcOptions>(options =>
            {
                //先移除Abp默认的
                var index = options.Filters.ToList()
                    .FindIndex(filter => filter is ServiceFilterAttribute attr && attr.ServiceType.Equals(typeof(AbpExceptionFilter)));
                if (index > -1)
                {
                    options.Filters.RemoveAt(index);
                }
                //再添加自定义的 -- 这里改用中间件
                //options.Filters.Add(typeof(BlogGlobalExceptionFilter));
            });
        }

        /// <summary>
        /// 跨域配置
        /// </summary>
        /// <param name="configuration"></param>
        private void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(BlogAppConsts.DefaultCorsPolicyName, builder =>
                {
                    builder.WithOrigins(
                            AppSettings.CorsOrigins.Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray()
                        )
                    .WithAbpExposedHeaders()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                    //AllowAnyOrigin()//允许任何来源的主机访问
                    //AllowCredentials()//指定处理cookie
                });
            });
        }

        /// <summary>
        /// JWT认证授权
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureAuthentication(IServiceCollection services)
        {
            var jwtAuthConfig = services.GetConfiguration().GetSection("JwtAuth");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,// 是否验证安全密钥
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtAuthConfig["SecurityKey"])),// 安全密钥

                    ValidateIssuer = true,// 是否验证颁发者
                    ValidIssuer = jwtAuthConfig["Issuer"],// 颁发者

                    ValidateAudience = true,// 是否验证访问群体
                    ValidAudience = jwtAuthConfig["Audience"],// 访问群体

                    ValidateLifetime = true,// 设置必须验证超时时间
                    RequireExpirationTime = true, //设置必须要有超时时间

                    ClockSkew = TimeSpan.FromSeconds(30)// 验证Token的有效时间的缓冲区
                    //TimeSpan.Zero 设置为0时，表示到期马上失效
                };

                // 应用程序提供的对象，用于处理承载引发的事件，身份验证处理程序
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = async context =>
                    {
                        context.HandleResponse();// 跳过默认的处理逻辑，返回下面的模型数据
                        context.Response.ContentType = "application/json;charset=utf-8";
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                        await context.Response.WriteAsync("{\"message\":\"Unauthorized\",\"success\":false}");
                    }
                };
            });
            services.AddAuthorization();
        }

        /// <summary>
        /// 自动API控制器
        /// </summary>
        private void ConfigureAutoApiControllers()
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers
                    .Create(typeof(BlogApplicationModule).Assembly, opts =>
                    {
                        //opts.RootPath = "blog";
                        opts.TypePredicate = type => { return false; };//过滤这些类以成为API控制器
                    });
            });
        }

        /// <summary>
        /// 路由配置
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureRouting(IServiceCollection services)
        {
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;// 设置URL为小写
                options.AppendTrailingSlash = true;// 在生成的URL后面添加斜杠
            });
        }
    }
}