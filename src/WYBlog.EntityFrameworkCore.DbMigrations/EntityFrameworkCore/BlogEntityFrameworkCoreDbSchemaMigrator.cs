using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using WYBlog.Data;

namespace WYBlog.EntityFrameworkCore
{
    [Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
    [ExposeServices(typeof(IBlogDbSchemaMigrator))]
    public class BlogEntityFrameworkCoreDbSchemaMigrator : IBlogDbSchemaMigrator
    {
        private readonly BlogMigrationsDbContext _dbContext;

        public BlogEntityFrameworkCoreDbSchemaMigrator(BlogMigrationsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task MigrateAsync()
        {
            await _dbContext.Database.MigrateAsync();
        }
    }
}