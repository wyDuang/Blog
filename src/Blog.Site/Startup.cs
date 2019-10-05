using System;
using System.IO;
using System.Text;
using AutoMapper;
using Blog.Core.Interfaces;
using Blog.Core.SettingModels;
using Blog.Infrastructure.CodeGenerator.CodeSettings;
using Blog.Infrastructure.Database;
using Blog.Infrastructure.Extensions;
using Blog.Infrastructure.Middlewares;
using Blog.Infrastructure.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog.Extensions.Logging;

namespace Blog.Site
{
    public class Startup
    {
        public Startup(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            _loggerFactory = loggerFactory;
        }

        private readonly string _defaultCorsPolicyName = "BlogApiCors";
        public static IConfiguration Configuration { get; set; }
        private readonly ILoggerFactory _loggerFactory;
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CodeGenerateSettings>(Configuration.GetSection("CodeGenerateSettings"));

            var jwtStrings = Configuration.GetSection("JwtSettings");
            services.Configure<JwtSettings>(jwtStrings);

            services.AddDbContext<MyContext>(options =>
            {
                var connectionString = Configuration.GetConnectionString("MySqlConnection");
                options.UseLazyLoadingProxies().UseMySql(connectionString);
            });

            services.AddCors(options =>
            {
                options.AddPolicy(_defaultCorsPolicyName, builder =>
                    builder.AllowAnyOrigin()//允许任何来源的主机访问
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           //.AllowCredentials()//指定处理cookie;
                           );
            });

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,//是否验证SecurityKey
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtStrings["IssuerSigningKey"])),//拿到SecurityKey
                    ValidateIssuer = true,//是否验证Issuer
                    ValidIssuer = jwtStrings["Issuer"],//Issuer，这两项和前面签发jwt的设置一致
                    ValidateAudience = true,//是否验证Audience
                    ValidAudience = jwtStrings["Audience"],//Audience
                    ValidateLifetime = true,//是否验证失效时间
                    ClockSkew = TimeSpan.FromMinutes(1) //默认5，是缓冲期，例如Token设置有效期为30，到了30分钟的时候是不会过期，会有5缓冲时间，也就是35分钟才会过期
                };
            });

            services.AddControllersWithViews(options =>
            {
                options.ReturnHttpNotAcceptable = true;//设为true,如果客户端请求不支持的数据格式,就会返回406
            }).AddJsonOptions(options =>
            {
                //options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }).AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<MyContext>());

            services.AddAutoMapper(typeof(Startup));

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUrlHelper>(factory =>
            {
                var actionContext = factory.GetService<IActionContextAccessor>().ActionContext;
                return new UrlHelper(actionContext);
            });

            services.AddResponseCaching();
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;// 采用小写的 URL 路由模式
                //options.AppendTrailingSlash = true; //最后面默认加斜杠
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0", new OpenApiInfo
                {
                    Title = "我的博客 API",
                    Version = "v1.0",
                    Description = "我的博客 API说明文档。",
                    Contact = new OpenApiContact
                    {
                        Name = "wyDuang",
                        Email = "110@wyduang.com",
                        Url = new Uri("https://wyduang.com"),
                    }
                });

                var basePath = Directory.GetCurrentDirectory();
                c.IncludeXmlComments(Path.Combine(basePath, "Blog.Site.xml"));
            });

            services.AddMyServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            _loggerFactory.AddNLog();
            app.UseMyExceptionHandler(_loggerFactory);

            if (env.IsDevelopment())
            {
                NLog.LogManager.LoadConfiguration("nlog.Development.config").GetCurrentClassLogger();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                NLog.LogManager.LoadConfiguration("nlog.config").GetCurrentClassLogger();
                NLog.LogManager.Configuration.Variables["connectionString"] = Configuration["DbOption:ConnectionString"].ToString();
            }
            app.UseCors(_defaultCorsPolicyName);
            app.UseStaticFiles();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "我的博客 API v1.0");
                //c.RoutePrefix = string.Empty;//在http://localhost:5000/处提供 Swagger UI
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                //endpoints.MapControllers();
            });
        }
    }
}
