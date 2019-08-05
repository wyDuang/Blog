using System;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using WyBlog.Web.Exceptions;
using WyBlog.Core.Models;
using WyBlog.Repository.MySql.Database;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace WyBlog.Web
{
    public class Startup
    {
        public Startup(
            IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            var connectionStrings = Configuration.GetSection("ConnectionStrings");
            services.Configure<ConnectionStrings>(connectionStrings);

            var jwtSection = Configuration.GetSection("JwtSettings");
            services.Configure<JwtSettings>(jwtSection);

            services.AddDbContext<BlogDbContext>(options =>
            {
                options.UseLazyLoadingProxies()
                    .UseMySql(connectionStrings["MySqlConnection"],
                        sqlOptions =>
                        {
                            sqlOptions.EnableRetryOnFailure(
                                maxRetryCount: 3,
                                maxRetryDelay: TimeSpan.FromSeconds(30),
                                errorNumbersToAdd: null);
                        });
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // 此lambda确定给定请求是否需要非必需cookie的用户同意。
                options.CheckConsentNeeded = context => false; 
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => {
                    options.LoginPath = "/login";
                    options.LogoutPath = "/logout";

                });

            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
            });

            //防止CSRF攻击
            services.AddAntiforgery(options =>
            {
                //使用cookiebuilder属性设置cookie属性。
                options.FormFieldName = "AntiforgeryKey_wyDuang";
                options.HeaderName = "X-CSRF-TOKEN-WYDUANG";
                options.SuppressXFrameOptionsHeader = false;
            });

            //解决MVC视图中的中文被html编码的问题
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));

            services.AddResponseCompression();

            services.AddMemoryCache();

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

            //小写路由url
            services.AddRouting(options => options.LowercaseUrls = true);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            //app.UseHttpsRedirection();
            app.UseCookiePolicy();
            app.UseResponseCompression();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
