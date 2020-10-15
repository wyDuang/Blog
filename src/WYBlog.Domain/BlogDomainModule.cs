using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;
using WYBlog.Configurations;

namespace WYBlog
{
    [DependsOn(
        typeof(BlogDomainSharedModule),
        typeof(AbpBackgroundJobsDomainModule)
        )]
    public class BlogDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            context.Services.Configure<GitHubConfig>(configuration.GetSection("GitHub"));
            context.Services.Configure<JwtAuthConfig>(configuration.GetSection("JwtAuth"));
        }
    }
}