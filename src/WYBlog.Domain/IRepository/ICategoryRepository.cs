using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using WYBlog.Entities;

namespace WYBlog.IRepository
{
    public interface ICategoryRepository : IRepository<Category, int>
    {
        Task<List<Category>> GetPagedListAsync(int skipCount, int maxResultCount, string sorting, string filter = null);

        Task<Category> FindByNameAsync(string name);

        Task<Category> FindByKeyAsync(string key);
    }
}