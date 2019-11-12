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
                    builder.AllowAnyOrigin()//�����κ���Դ����������
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           //.AllowCredentials()//ָ������cookie;
                           );
            });

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,//�Ƿ���֤SecurityKey
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtStrings["IssuerSigningKey"])),//�õ�SecurityKey
                    ValidateIssuer = true,//�Ƿ���֤Issuer
                    ValidIssuer = jwtStrings["Issuer"],//Issuer���������ǰ��ǩ��jwt������һ��
                    ValidateAudience = true,//�Ƿ���֤Audience
                    ValidAudience = jwtStrings["Audience"],//Audience
                    ValidateLifetime = true,//�Ƿ���֤ʧЧʱ��
                    ClockSkew = TimeSpan.FromMinutes(1) //Ĭ��5���ǻ����ڣ�����Token������Ч��Ϊ30������30���ӵ�ʱ���ǲ�����ڣ�����5����ʱ�䣬Ҳ����35���ӲŻ����
                };
            });

            services.AddControllersWithViews(options =>
            {
                options.ReturnHttpNotAcceptable = true;//��Ϊtrue,����ͻ�������֧�ֵ����ݸ�ʽ,�ͻ᷵��406
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
                options.LowercaseUrls = true;// ����Сд�� URL ·��ģʽ
                //options.AppendTrailingSlash = true; //�����Ĭ�ϼ�б��
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0", new OpenApiInfo
                {
                    Title = "�ҵĲ��� API",
                    Version = "v1.0",
                    Description = "�ҵĲ��� API˵���ĵ���",
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
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "�ҵĲ��� API v1.0");
                //c.RoutePrefix = string.Empty;//��http://localhost:5000/���ṩ Swagger UI
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
