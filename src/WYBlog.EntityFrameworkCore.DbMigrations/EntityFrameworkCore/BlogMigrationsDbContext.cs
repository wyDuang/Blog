using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Volo.Abp.EntityFrameworkCore;

namespace WYBlog.EntityFrameworkCore
{
    public class BlogMigrationsDbContext : AbpDbContext<BlogMigrationsDbContext>
    {
        public BlogMigrationsDbContext(DbContextOptions<BlogMigrationsDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 批量注入EntityTypeConfiguration
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}