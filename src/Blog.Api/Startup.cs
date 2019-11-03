using System;
using System.Text;
using AutoMapper;
using Blog.Core.SettingModels;
using Blog.Infrastructure.Database;
using Blog.Infrastructure.Extensions;
using Blog.Infrastructure.Swagger;
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
            services.AddControllers();

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

            services.AddSwagger();
            services.AddAuthentication(
                options =>
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
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtStrings["IssuerSigningKey"])),//�õ�SecurityKey
                        ClockSkew = TimeSpan.FromMinutes(1) //Ĭ��5���ӻ����ڣ�����Token������Ч��Ϊ30������30���ӵ�ʱ���ǲ�����ڣ�����5����ʱ�䡣
                    };
                });
            services.AddAuthorization();
            //services.AddResponseCaching();
            services.AddMemoryCache();
            services.AddCors(options =>
            {
                options.AddPolicy(_defaultCorsPolicyName, builder =>
                    builder.AllowAnyOrigin()//�����κ���Դ����������
                           .AllowAnyMethod()
                           .AllowAnyHeader());//.AllowCredentials()//ָ������cookie;
            });
            services.AddHttpClient();

            services.AddAutoMapper(typeof(Startup));
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpContextAccessor();            
            services.AddScoped<IUrlHelper>(factory =>
            {
                var actionContext = factory.GetService<IActionContextAccessor>().ActionContext;
                return new UrlHelper(actionContext);
            });
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
            //app.UseResponseCaching();
            app.UseCors(_defaultCorsPolicyName);
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwagger().UseSwaggerUI();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
