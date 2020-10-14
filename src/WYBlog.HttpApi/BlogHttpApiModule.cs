using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.AspNetCore.Mvc;

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
            context.Services.AddHttpClient();
        }
    }
}