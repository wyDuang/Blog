using System.Threading.Tasks;
using WyBlog.Core.Models;
using WyBlog.Entities;

namespace WyBlog.IRepository
{
    public interface IArticleRepository : IBaseRepository<Article, int>
    {
        Task<PaginatedList<Article>> GetPageListAsync(PaginatedQuery paginatedQuery);
    }
}
