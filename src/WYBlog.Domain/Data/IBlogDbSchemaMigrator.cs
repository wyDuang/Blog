using System.Threading.Tasks;

namespace WYBlog.Data
{
    public interface IBlogDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}