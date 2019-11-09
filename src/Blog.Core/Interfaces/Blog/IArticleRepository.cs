using Blog.Core.BaseModels;
using Blog.Core.Entities;
using Blog.Core.Interfaces;
using Blog.Core.QueryParameters;
using System.Threading.Tasks;

namespace Blog.Core.Interfaces
{
    public interface IArticleRepository : IRepositoryBase<Article>
    {
        Task<PaginatedList<Article>> GetPageListAsync(ArticleParameter parameter, IPropertyMapping propertyMapping);
    }
}
