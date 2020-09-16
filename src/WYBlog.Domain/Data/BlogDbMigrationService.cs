using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace WYBlog.Data
{
    public class BlogDbMigrationService : ITransientDependency
    {
        public ILogger<BlogDbMigrationService> Logger { get; set; }

        private readonly IDataSeeder _dataSeeder;
        private readonly IBlogDbSchemaMigrator _dbSchemaMigrator;

        public BlogDbMigrationService(
            IDataSeeder dataSeeder,
            IBlogDbSchemaMigrator dbSchemaMigrator)
        {
            _dataSeeder = dataSeeder;
            _dbSchemaMigrator = dbSchemaMigrator;

            Logger = NullLogger<BlogDbMigrationService>.Instance;
        }

        public async Task MigrateAsync()
        {
            Logger.LogInformation(">>>>>>>> 开始数据库迁移...");

            Logger.LogInformation("迁移数据库架构...");
            await _dbSchemaMigrator.MigrateAsync();

            Logger.LogInformation("执行数据库种子...");
            await _dataSeeder.SeedAsync();

            Logger.LogInformation(">>>>>>>> 成功完成数据库迁移。");
        }
    }
}