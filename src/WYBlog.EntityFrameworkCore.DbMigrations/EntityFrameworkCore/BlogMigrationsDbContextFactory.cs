using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WYBlog.EntityFrameworkCore
{
    public class BlogMigrationsDbContextFactory: IDesignTimeDbContextFactory<BlogMigrationsDbContext>
    {
        public BlogMigrationsDbContext CreateDbContext(string[] args)
        {
            //BookStoreEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<BlogMigrationsDbContext>()
                .UseMySql(configuration.GetConnectionString("Default"));

            return new BlogMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../WYBlog.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
