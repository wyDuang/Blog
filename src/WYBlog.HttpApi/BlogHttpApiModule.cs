using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace WYBlog
{
    [DependsOn(
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