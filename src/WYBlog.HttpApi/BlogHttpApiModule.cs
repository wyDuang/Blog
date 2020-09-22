using Microsoft.Extensions.DependencyInjection;
using System;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace WYBlog
{
    [DependsOn(
        typeof(BlogApplicationContractsModule),
        typeof(AbpHttpClientModule)
        )]
    public class BlogHttpApiModule : AbpModule
    {
        public const string RemoteServiceName = "Default";
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(BlogApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
