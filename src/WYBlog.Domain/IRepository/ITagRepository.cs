using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using WYBlog.Entities;

namespace WYBlog.IRepository
{
    public interface ITagRepository : IRepository<Tag, int>
    {
        Task<List<Tag>> GetPagedListAsync(int skipCount, int maxResultCount, string sorting, string filter = null);

        Task<Tag> FindByNameAsync(string name);

        Task<Tag> FindByKeyAsync(string key);
    }
}