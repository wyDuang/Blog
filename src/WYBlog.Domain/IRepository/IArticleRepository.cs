using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using WYBlog.Entities;

namespace WYBlog.IRepository
{
    public interface IArticleRepository : IRepository<Article, int>
    {
        Task<List<Article>> GetPagedListAsync(int skipCount, int maxResultCount, string sorting, string filter = null);

        Task<Article> FindByTitleAsync(string title);

        Task<Article> FindByKeyAsync(string key);

        Task<IQueryable<Article>> QueryableListAsync();
    }
}