using System.Linq;
using System.Threading.Tasks;
using Blog.Core.BaseModels;
using Blog.Core.Entities;
using Blog.Core.Interfaces;
using Blog.Core.QueryParameters;
using Blog.Infrastructure.Database;
using Blog.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Repositories
{
    public class ArticleRepository : EFRepository<Article>, IArticleRepository
    {
        public ArticleRepository(MyContext myContext) : base(myContext)
        {
        }

        public async Task<PaginatedList<Article>> GetPageListAsync(ArticleParameter parameter, IPropertyMapping propertyMapping)
        {
            var query = Context.Articles.AsQueryable();

            if (!string.IsNullOrEmpty(parameter.Title))
            {
                var title = parameter.Title.ToLowerInvariant();
                query = query.Where(x => x.Title.ToLowerInvariant() == title);
            }

            query = query.ApplySort(parameter.OrderBy, propertyMapping);

            var count = await query.CountAsync();
            var data = await query
                .Skip(parameter.PageIndex * parameter.PageSize)
                .Take(parameter.PageSize)
                .ToListAsync();

            return new PaginatedList<Article>(parameter.PageIndex, parameter.PageSize, count, data);
        }
    }
}
