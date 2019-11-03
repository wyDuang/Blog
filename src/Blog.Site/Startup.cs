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
                    builder.AllowAnyOrigin()//�����κ���Դ����������
                           .AllowAnyMethod()
                           .AllowAnyHeader());//.AllowCredentials()//ָ������cookie;
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
            //        ValidateIssuerSigningKey = true,//�Ƿ���֤SecurityKey
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtStrings["IssuerSigningKey"])),//�õ�SecurityKey
            //        ValidateIssuer = true,//�Ƿ���֤Issuer
            //        ValidIssuer = jwtStrings["Issuer"],//Issuer���������ǰ��ǩ��jwt������һ��
            //        ValidateAudience = true,//�Ƿ���֤Audience
            //        ValidAudience = jwtStrings["Audience"],//Audience
            //        ValidateLifetime = true,//�Ƿ���֤ʧЧʱ��
            //        ClockSkew = TimeSpan.FromMinutes(1) //Ĭ��5���ǻ����ڣ�����Token������Ч��Ϊ30������30���ӵ�ʱ���ǲ�����ڣ�����5����ʱ�䣬Ҳ����35���ӲŻ����
            //    };
            //});

            services.AddControllersWithViews(options =>
            {
                options.ReturnHttpNotAcceptable = true;//��Ϊtrue,����ͻ�������֧�ֵ����ݸ�ʽ,�ͻ᷵��406
            })
                .AddRazorRuntimeCompilation()
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null)//Ĭ��CamelCase�������ĸСд���ڴ˽��ĳ�PascalCase�������ĸ��д
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
                options.LowercaseUrls = true;// ����Сд�� URL ·��ģʽ
                //options.AppendTrailingSlash = true; //�����Ĭ�ϼ�б��
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "�ҵĲ��� API",
                    Description = "�ҵĲ��� API˵���ĵ���",
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
                    Description = "�����ֶ������뵥�ʡ�bearer��������ո��jwtֵ",
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
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "�ҵĲ���API v1");

                    //c.RoutePrefix = string.Empty;//��http://localhost:5000/���ṩ Swagger UI
                    c.DocumentTitle = "�ҵĲ��� API";
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
