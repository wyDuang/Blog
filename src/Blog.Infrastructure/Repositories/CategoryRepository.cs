
using Blog.Core.BaseModels;
using Blog.Core.Entities;
using Blog.Core.Interfaces;
using Blog.Core.QueryParameters;
using Blog.Infrastructure.Database;
using Blog.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Repositories
{
    public class CategoryRepository : EFRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(MyContext myContext) : base(myContext)
        {
        }

        public async Task<PaginatedList<Category>> GetPageListAsync(CategoryParameter parameter, IPropertyMapping propertyMapping = null)
        {
            var query = dbSet.AsQueryable();

            if (!string.IsNullOrEmpty(parameter.Name))
            {
                var name = parameter.Name.ToLowerInvariant();
                query = query.Where(x => x.CategoryName.ToLowerInvariant() == name);
            }

            if (propertyMapping != null)
            {
                query = query.ApplySort(parameter.OrderBy, propertyMapping);
            }

            var count = await query.CountAsync();
            var data = await query
                .Skip(parameter.PageIndex * parameter.PageSize)
                .Take(parameter.PageSize)
                .ToListAsync();

            return new PaginatedList<Category>(parameter.PageIndex, parameter.PageSize, count, data);
        }
    }
}
