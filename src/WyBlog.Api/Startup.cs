using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.Swagger;

namespace WyBlog.Api
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
            services.AddControllers();

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new Info {
            //        Title = "My Blog API",
            //        Version = "v1",
            //        Description = "我的博客咿呀咿呀呦！",
            //        TermsOfService = "None",
            //        Contact = new Contact
            //        {
            //            Name = "Shayne Boyer",
            //            Email = string.Empty,
            //            Url = "https://twitter.com/spboyer"
            //        },
            //        License = new License
            //        {
            //            Name = "Use under LICX",
            //            Url = "https://example.com/license"
            //        }
            //    });

            //    //// 生成与 Web API 项目相匹配的 XML 文件名（因为Linux上区分大小写）
            //    //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //    //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            //    //c.IncludeXmlComments(xmlPath);
            //});
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //app.UseStaticFiles();
            //app.UseSwagger();
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Blog API V1");
            //});

            //app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
