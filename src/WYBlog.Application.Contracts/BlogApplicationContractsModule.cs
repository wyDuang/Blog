using Volo.Abp.FluentValidation;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;

namespace WYBlog
{
    [DependsOn(
        typeof(BlogDomainSharedModule),
        typeof(AbpFluentValidationModule),
        typeof(AbpPermissionManagementApplicationContractsModule)
        )]
    public class BlogApplicationContractsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            BlogDtoExtensions.Configure();
        }
    }
}