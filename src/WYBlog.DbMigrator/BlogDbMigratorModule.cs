using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using WYBlog.EntityFrameworkCore;

namespace WYBlog
{
    [DependsOn(
       typeof(AbpAutofacModule),
       typeof(BlogApplicationContractsModule),
       typeof(BlogEntityFrameworkCoreDbMigrationsModule)
       )]
    public class BlogDbMigratorModule : AbpModule
    {
    }
}