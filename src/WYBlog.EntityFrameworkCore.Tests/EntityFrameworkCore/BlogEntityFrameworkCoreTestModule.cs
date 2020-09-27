using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.Modularity;
using WYBlog.TestBase;

namespace WYBlog.EntityFrameworkCore
{
    [DependsOn(
        typeof(BlogEntityFrameworkCoreDbMigrationsModule),
        typeof(BlogTestBaseModule),
        typeof(AbpEntityFrameworkCoreMySQLModule)
        )]
    public class BlogEntityFrameworkCoreTestModule : AbpModule
    {
        private MySqlConnection _mySqlConnection;
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            ConfigureInMemoryMySql(context.Services);
        }

        private void ConfigureInMemoryMySql(IServiceCollection services)
        {
            _mySqlConnection = CreateDatabaseAndGetConnection();

            services.Configure<AbpDbContextOptions>(options =>
            {
                options.Configure(context =>
                {
                    context.DbContextOptions.UseSqlite(_sqliteConnection);
                });
            });
        }

        private static SqliteConnection CreateDatabaseAndGetConnection()
        {

        }
    }
}