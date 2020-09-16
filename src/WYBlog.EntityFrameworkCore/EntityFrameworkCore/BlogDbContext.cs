using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace WYBlog.EntityFrameworkCore
{
    [ConnectionStringName("Default")]
    public class BlogDbContext : AbpDbContext<BlogDbContext>
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureBlog();
        }
    }
}