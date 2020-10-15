using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace WYBlog
{
    [DependsOn(
        typeof(AbpAspNetCoreMvcModule),
        typeof(BlogApplicationContractsModule)
        )]
    public class BlogHttpApiModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
        }
    }
}