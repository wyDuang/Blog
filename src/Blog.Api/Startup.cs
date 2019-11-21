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
                options.LowercaseUrls = true;//true������Сд��URL·��ģʽ
                options.AppendTrailingSlash = false; //true��URL�����Ĭ�ϼ�б��
            });

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;// ����ѭ������
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();//��ʹ���շ�
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";// ����ʱ���ʽ

                    //options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;// ���ֶ�Ϊnullֵ�����ֶβ��᷵�ص�ǰ��
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
                    ValidateIssuerSigningKey = true,//�Ƿ���֤SecurityKey
                    ValidateIssuer = true,//�Ƿ���֤Issuer
                    ValidateAudience = true,//�Ƿ���֤Audience
                    ValidateLifetime = true,//�Ƿ���֤ʧЧʱ�� 
                    ValidIssuer = jwtStrings["Issuer"],//Issuer���������ǰ��ǩ��jwt������һ��
                    ValidAudience = jwtStrings["Audience"],//Audience
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtStrings["IssuerSigningKey"])),//�õ�SecurityKey
                    ClockSkew = TimeSpan.FromMinutes(1), //Ĭ��5���ӻ����ڣ�����Token������Ч��Ϊ30������30���ӵ�ʱ���ǲ�����ڣ�����5����ʱ�䡣
                    RequireExpirationTime = true,//�����й���ʱ��
                };
            });

            services.AddCors(options =>
            {
                options.AddPolicy(_defaultCorsPolicyName, builder =>
                    builder.AllowAnyOrigin()//�����κ���Դ����������
                           .AllowAnyMethod()
                           .AllowAnyHeader());//.AllowCredentials()//ָ������cookie;
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
