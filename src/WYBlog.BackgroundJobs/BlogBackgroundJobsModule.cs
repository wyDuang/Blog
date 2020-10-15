using Hangfire;
using Hangfire.Dashboard.BasicAuthorization;
using Hangfire.MySql.Core;
using MailKit.Security;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.AspNetCore;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.BackgroundJobs.Hangfire;
using Volo.Abp.MailKit;
using Volo.Abp.Modularity;

namespace WYBlog
{
    [DependsOn(
        typeof(AbpMailKitModule),
        typeof(AbpAspNetCoreModule),
        typeof(AbpBackgroundJobsHangfireModule),
        typeof(BlogDomainModule),
        typeof(BlogApplicationContractsModule)
    )]
    public class BlogBackgroundJobsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);

            ConfigureMailKit();
            ConfigureHangfire(context.Services);
        }

        /// <summary>
        /// MailKit
        /// </summary>
        private void ConfigureMailKit()
        {
            Configure<AbpMailKitOptions>(options =>
            {
                options.SecureSocketOption = SecureSocketOptions.StartTls;
            });
        }

        /// <summary>
        /// Hangfire面板
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureHangfire(IServiceCollection services)
        {
            var hangfireConfig = services.GetConfiguration().GetSection("Hangfire");

            services.AddHangfire(config =>
            {
                config.UseStorage(
                            new MySqlStorage(hangfireConfig["ConnectionString"],
                            new MySqlStorageOptions
                            {
                                TablePrefix = ""
                            }));
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var env = context.GetEnvironment();
            var app = context.GetApplicationBuilder();
            var hangfireConfig = context.GetConfiguration().GetSection("Hangfire");

            app.UseHangfireServer();

            app.UseHangfireDashboard(options: new DashboardOptions
            {
                Authorization = new[]
                    {
                        new BasicAuthAuthorizationFilter(new BasicAuthAuthorizationFilterOptions
                        {
                            RequireSsl = false,
                            SslRedirect = false,
                            LoginCaseSensitive = true,
                            Users = new []
                            {
                                new BasicAuthAuthorizationUser
                                {
                                    Login = hangfireConfig["Login"],
                                    PasswordClear =  hangfireConfig["Password"]
                                }
                            }
                        })
                    },
                DashboardTitle = "任务调度中心"
            });

            //var service = context.ServiceProvider;
            //service.UseHangfireTest();
        }
    }
}