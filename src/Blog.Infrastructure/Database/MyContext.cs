using Microsoft.EntityFrameworkCore;
using Blog.Core.Entities;
using Blog.Infrastructure.Database.EntityConfigurations;
using System.Reflection;
using System.Linq;
using Blog.Core.Interfaces;
using Blog.Infrastructure.Extensions;

namespace Blog.Infrastructure.Database
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.MappingEntityTypes();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ArticleConfiguration).Assembly);
        }
    }
}
