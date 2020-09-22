using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Volo.Abp;

namespace WYBlog.EntityFrameworkCore
{
    public static class BlogDbContextModelCreatingExtensions
    {
        public static void ConfigureBlog(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            // 批量注入EntityTypeConfiguration
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}