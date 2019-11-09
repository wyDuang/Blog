using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AutoMapper;
using Blog.Core.SettingModels;
using Blog.Infrastructure.CodeGenerator.CodeSettings;
using Blog.Infrastructure.Database;
using Blog.Infrastructure.Extensions;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using NLog.Extensions.Logging;
using NLog.Web;

namespace Blog.Site
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
            services.Configure<CodeGenerateSettings>(Configuration.GetSection("CodeGenerateSettings"));

            var jwtStrings = Configuration.GetSection("JwtSettings");
            services.Configure<JwtSettings>(jwtStrings);

            //services.Configure<DomainSettings>(Configuration.GetSection("SubDomains"));

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
                           .AllowAnyHeader());//.AllowCredentials()//指定处理cookie;
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.LoginPath = "/login";
                });

            services.AddSession();
            services.AddHttpClient();
            services.AddMemoryCache();

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,//是否验证SecurityKey
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtStrings["IssuerSigningKey"])),//拿到SecurityKey
            //        ValidateIssuer = true,//是否验证Issuer
            //        ValidIssuer = jwtStrings["Issuer"],//Issuer，这两项和前面签发jwt的设置一致
            //        ValidateAudience = true,//是否验证Audience
            //        ValidAudience = jwtStrings["Audience"],//Audience
            //        ValidateLifetime = true,//是否验证失效时间
            //        ClockSkew = TimeSpan.FromMinutes(1) //默认5，是缓冲期，例如Token设置有效期为30，到了30分钟的时候是不会过期，会有5缓冲时间，也就是35分钟才会过期
            //    };
            //});

            services.AddControllersWithViews(options =>
            {
                options.ReturnHttpNotAcceptable = true;//设为true,如果客户端请求不支持的数据格式,就会返回406
            })
                .AddRazorRuntimeCompilation()
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null)//默认CamelCase风格，首字母小写，在此将改成PascalCase风格，首字母大写
                //.AddNewtonsoftJson(options => {
                //    options.SerializerSettings.ContractResolver = new DefaultContractResolver(){
                //        NamingStrategy = new DefaultNamingStrategy() 
                //    };
                //})
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<MyContext>());

            services.AddAutoMapper(typeof(Startup));

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpContextAccessor();

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUrlHelper>(factory =>
            {
                var actionContext = factory.GetService<IActionContextAccessor>().ActionContext;
                return new UrlHelper(actionContext);
            });

            //services.AddResponseCaching();
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;// 采用小写的 URL 路由模式
                //options.AppendTrailingSlash = true; //最后面默认加斜杠
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "我的博客 API",
                    Description = "我的博客 API说明文档。",
                    Contact = new OpenApiContact
                    {
                        Name = "wyDuang",
                        Email = "110@wyduang.com",
                        Url = new Uri("https://wyduang.com"),
                    }
                });

                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{typeof(Startup).Assembly.GetName().Name}.xml"), true);

                //var basePath = Directory.GetCurrentDirectory();
                //c.IncludeXmlComments(Path.Combine(basePath, "Blog.Site.xml"));

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() {
                    Description = "请在字段中输入单词“bearer”，后跟空格和jwt值",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    { new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference()
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    }, Array.Empty<string>() }
                });
            });

            services.AddMyServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                NLogBuilder.ConfigureNLog("nlog.Development.config").GetCurrentClassLogger();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
                app.UseExceptionHandler("/Home/Error");
            }
            
            app.UseStaticFiles();

            app.UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "我的博客API v1");

                    //c.RoutePrefix = string.Empty;//在http://localhost:5000/处提供 Swagger UI
                    c.DocumentTitle = "我的博客 API";
                });

            app.UseRouting();
            app.UseCors(_defaultCorsPolicyName);

            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                     name: "default",
                     pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
