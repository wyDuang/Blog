using AutoMapper;
using Blog.Core.Interfaces;
using Blog.Infrastructure.CodeGenerator;
using Blog.Infrastructure.CodeGenerator.CodeSettings;
using Blog.Infrastructure.Database;
using Blog.Infrastructure.Extensions;
using Blog.Infrastructure.Repositories;
using Blog.Infrastructure.Resources;
using Blog.Infrastructure.Resources.Validators;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
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

        public void ConfigureServices(IServiceCollection services)
        {
            //services.Configure<CodeGenerateSettings>(Configuration.GetSection("CodeGenerateSettings"));
            //services.AddScoped<CodeGenerator>();

            services.AddDbContext<MyContext>(options =>
            {
                var connectionString = Configuration.GetConnectionString("MySqlConnection");
                options.UseLazyLoadingProxies().UseMySql(connectionString);
            });

            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            //    {
            //        options.LoginPath = "/login";
            //    });

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

            services.AddMemoryCache();
            services.AddControllersWithViews(options =>
            {
                options.ReturnHttpNotAcceptable = true;//设为true,如果客户端请求不支持的数据格式,就会返回406
            })
                .AddRazorRuntimeCompilation()//并且在程序启动时，需要启动运行时编译的功能
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);//默认CamelCase风格，首字母小写，在此将改成PascalCase风格，首字母大写
                //.AddNewtonsoftJson(options => {
                //    options.SerializerSettings.ContractResolver = new DefaultContractResolver(){
                //        NamingStrategy = new DefaultNamingStrategy() 
                //    };
                //})

            services.AddAutoMapper(typeof(Startup));

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpContextAccessor();
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
                options.AppendTrailingSlash = false; //默认true 后面加斜杠
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
            app.UseRouting();  
            
            //app.UseAuthentication();
            //app.UseAuthorization();
            
            app.UseResponseCaching();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                     name: "default",
                     pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
