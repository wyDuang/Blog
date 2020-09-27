using Hangfire;
using Hangfire.Dashboard.BasicAuthorization;
using Hangfire.MySql.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Volo.Abp.BackgroundJobs.Hangfire;
using Volo.Abp.Modularity;
using WYBlog.Configurations;

namespace WYBlog.BackgroundJobs
{
    [DependsOn(
        typeof(AbpBackgroundJobsHangfireModule),
        typeof(BlogDomainModule)
    )]
    public class BlogBackgroundJobsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            context.Services.AddHangfire(config =>
            {
                config.UseStorage(
                            new MySqlStorage(configuration.GetConnectionString("Default"),
                            new MySqlStorageOptions
                            {
                                TablePrefix = "wy_hangfire"
                            }));
            });
        }
    }
}
