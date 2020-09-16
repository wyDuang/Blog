using Volo.Abp.FluentValidation;
using Volo.Abp.Modularity;

namespace WYBlog
{
    [DependsOn(
        typeof(BlogDomainSharedModule),
        typeof(AbpFluentValidationModule)
        )]
    public class BlogApplicationContractsModule : AbpModule
    {
    }
}