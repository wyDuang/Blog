using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.Modularity;

namespace WYBlog.EntityFrameworkCore
{
    [DependsOn(
        typeof(BlogDomainModule),
        typeof(AbpEntityFrameworkCoreMySQLModule)
        )]
    public class BlogEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<BlogDbContext>(options =>
            {
                options.AddDefaultRepositories(includeAllEntities: true);
            });

            Configure<AbpDbContextOptions>(options =>
            {
                options.UseMySQL();
            });
        }
    }
}