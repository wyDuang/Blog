using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

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
        }
    }
}