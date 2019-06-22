using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using WyBlog.Api.Exceptions;
using WyBlog.Core.Models;

namespace WyBlog.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            var JwtSection = Configuration.GetSection("JwtSettings");
            services.Configure<JwtSettings>(JwtSection);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                {
                    //if (options.SerializerSettings.ContractResolver is DefaultContractResolver resolver)
                    //{
                    //    resolver.NamingStrategy = null;//去掉默认的驼峰式命名(首字母小写)
                    //}

                    //塑形后字段首字母都变成了大写，这里设置后首字母全部变成小写
                    //options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = JwtSection["Issuer"],//Token颁发机构
                        ValidAudience = JwtSection["Audience"],//颁发给谁
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSection["SecurityKey"])),

                        /**********TokenValidationParameters的参数默认值**********/
                        ValidateIssuer = true,//是否验证Issuer
                        ValidateAudience = true,//是否验证Audience
                        ValidateLifetime = true,//是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比
                        ValidateIssuerSigningKey = true,//是否验证SecurityKey

                        RequireExpirationTime = true,// 是否要求Token的Claims中必须包含Expires
                        ClockSkew = TimeSpan.FromSeconds(300),// 允许的服务器时间偏移量
                    };
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My Blog API", Version = "v1",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = "None",//服务条款
                    Contact = new Contact
                    {
                        Name = "wyDuang",
                        Email = "1014558384@qq.com",
                        Url = "https://api.wyduang.com"
                    }
                });

                //var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);//默认的第二个参数是false，这个是controller的注释，记得修改
            });

            //services.AddRouting(options => {
            //    options.LowercaseUrls = true;//url地址全部转成小写
            //});

            services.AddHttpClient();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            
            #region 自定义api全局异常处理类
            app.UseMyExceptionHandler(loggerFactory);
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();//这种对MVC项目比较友好，但是不适合Api，Api建议所有的返回都是json格式
            //}
            #endregion

            app.UseAuthentication();
            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Blog API V1");
            });

            app.UseMvc();

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
