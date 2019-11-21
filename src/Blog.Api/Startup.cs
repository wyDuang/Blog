using System;
using System.Linq;
using System.Reflection;
using System.Text;
using AutoMapper;
using Blog.Core.Interfaces;
using Blog.Core.SettingModels;
using Blog.Infrastructure.Database;
using Blog.Infrastructure.Extensions;
using Blog.Infrastructure.Repositories;
using Blog.Infrastructure.Resources;
using Blog.Infrastructure.Resources.PropertyMappings;
using Blog.Infrastructure.Resources.Validators;
using Blog.Infrastructure.Services;
using Blog.Infrastructure.Swagger;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NLog.Web;

namespace Blog.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private readonly string _defaultCorsPolicyName = "BlogApiCors";

        public void ConfigureServices(IServiceCollection services)
        {
            var jwtStrings = Configuration.GetSection("JwtSettings");
            services.Configure<JwtSettings>(jwtStrings);

            services.AddDbContext<MyContext>(options =>
            {
                var connectionString = Configuration.GetConnectionString("MySqlConnection");
                options.UseLazyLoadingProxies().UseMySql(connectionString);
            });

            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;//true：采用小写的URL路由模式
                options.AppendTrailingSlash = false; //true：URL最后面默认加斜杠
            });

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;// 忽略循环引用
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();//不使用驼峰
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";// 设置时间格式

                    //options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;// 如字段为null值，该字段不会返回到前端
                })
                .AddFluentValidation();

            var types = Assembly.GetExecutingAssembly().GetTypes().Where(p => p.BaseType.GetInterfaces().Any(x => x == typeof(IValidator)));
            foreach (var type in types)
            {
                if (type.BaseType != null)
                {
                    var genericType = typeof(IValidator<>).MakeGenericType(type.BaseType.GenericTypeArguments[0]);
                    services.AddTransient(genericType, type);
                }
            }

            services.AddSwagger();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,//是否验证SecurityKey
                    ValidateIssuer = true,//是否验证Issuer
                    ValidateAudience = true,//是否验证Audience
                    ValidateLifetime = true,//是否验证失效时间 
                    ValidIssuer = jwtStrings["Issuer"],//Issuer，这两项和前面签发jwt的设置一致
                    ValidAudience = jwtStrings["Audience"],//Audience
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtStrings["IssuerSigningKey"])),//拿到SecurityKey
                    ClockSkew = TimeSpan.FromMinutes(1), //默认5分钟缓冲期，例如Token设置有效期为30，到了30分钟的时候是不会过期，会有5缓冲时间。
                    RequireExpirationTime = true,//必须有过期时间
                };
            });

            services.AddCors(options =>
            {
                options.AddPolicy(_defaultCorsPolicyName, builder =>
                    builder.AllowAnyOrigin()//允许任何来源的主机访问
                           .AllowAnyMethod()
                           .AllowAnyHeader());//.AllowCredentials()//指定处理cookie;
            });

            services.AddAutoMapper(typeof(Startup));
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpContextAccessor();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUrlHelper>(factory =>
            {
                var actionContext = factory.GetService<IActionContextAccessor>().ActionContext;
                return new UrlHelper(actionContext);
            });

            var propertyMappingContainer = new PropertyMappingContainer();
            propertyMappingContainer.Register<ArticlePropertyMapping>();
            services.AddSingleton<IPropertyMappingContainer>(propertyMappingContainer);
            services.AddTransient<ITypeHelperService, TypeHelperService>();

            services.AddMyServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                NLogBuilder.ConfigureNLog("nlog.Development.config").GetCurrentClassLogger();
                app.UseMyExceptionHandler(loggerFactory);
            }
            else
            {
                NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseRouting();
            app.UseCors(_defaultCorsPolicyName);
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseSwagger()
                .UseSwaggerUI();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
