using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;

namespace WYBlog
{
    [DependsOn(
        typeof(AbpBackgroundJobsDomainSharedModule),
        typeof(AbpPermissionManagementDomainSharedModule)
        )]
    public class BlogDomainSharedModule : AbpModule
    {
    }
}