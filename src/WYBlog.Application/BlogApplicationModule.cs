using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace WYBlog
{
    [DependsOn(
        typeof(BlogDomainModule),
        typeof(BlogApplicationContractsModule),
        typeof(AbpAutoMapperModule)
        )]
    public class BlogApplicationModule : AbpModule
    {
    }
}