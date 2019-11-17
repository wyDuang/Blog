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

            services.AddMemoryCache();
            services.AddControllersWithViews(options =>
            {
                options.ReturnHttpNotAcceptable = true;//��Ϊtrue,����ͻ�������֧�ֵ����ݸ�ʽ,�ͻ᷵��406
            })
                .AddRazorRuntimeCompilation()//�����ڳ�������ʱ����Ҫ��������ʱ����Ĺ���
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);//Ĭ��CamelCase�������ĸСд���ڴ˽��ĳ�PascalCase�������ĸ��д
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
                options.LowercaseUrls = true;// ����Сд�� URL ·��ģʽ
                options.AppendTrailingSlash = false; //Ĭ��true �����б��
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
