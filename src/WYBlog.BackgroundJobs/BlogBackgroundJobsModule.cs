using Hangfire;
using Hangfire.Dashboard.BasicAuthorization;
using Hangfire.MySql.Core;
using MailKit.Security;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Volo.Abp;
using Volo.Abp.BackgroundJobs.Hangfire;
using Volo.Abp.MailKit;
using Volo.Abp.Modularity;
using WYBlog.Configurations;

namespace WYBlog
{
    [DependsOn(
        typeof(AbpMailKitModule),
        typeof(AbpBackgroundJobsHangfireModule),
        typeof(BlogApplicationContractsModule)
    )]
    public class BlogBackgroundJobsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
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
            services.AddHangfire(config =>
            {
                config.UseStorage(
                            new MySqlStorage(AppSettings.Hangfire.ConnectionString,
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
                                    Login = AppSettings.Hangfire.Login,
                                    PasswordClear =  AppSettings.Hangfire.Password
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