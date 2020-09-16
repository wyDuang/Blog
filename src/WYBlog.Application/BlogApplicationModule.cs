using Volo.Abp.Modularity;

namespace WYBlog
{
    [DependsOn(
        typeof(BlogDomainModule),
        typeof(BlogApplicationContractsModule)
        )]
    public class BlogApplicationModule : AbpModule
    {
    }
}