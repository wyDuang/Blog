using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace WYBlog.Data
{
    public class NullBookStoreDbSchemaMigrator : IBlogDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}