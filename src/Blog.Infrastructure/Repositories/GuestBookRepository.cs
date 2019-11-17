using Blog.Core.BaseModels;
using Blog.Core.Entities;
using Blog.Core.Interfaces;
using Blog.Infrastructure.Database;
using Blog.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Repositories
{
    /// <summary>
    /// 留言表（仓储层实现）
    /// </summary>
    public class GuestbookRepository : EFRepository<GuestBook>, IGuestBookRepository
    {
        public GuestbookRepository(MyContext myContext) : base(myContext)
        {
        }

        public async Task<PaginatedList<GuestBook>> GetPageListAsync(GuestBookParameter parameter, IPropertyMapping propertyMapping = null)
        {
			var query = Context.Set<GuestBook>().AsQueryable();

            //TODO: 条件

            if (propertyMapping != null)
            {
                query = query.ApplySort(parameter.OrderBy, propertyMapping);
            }

            var count = await query.CountAsync();
			var data = await query
                .Skip(parameter.PageIndex * parameter.PageSize)
                .Take(parameter.PageSize)
                .ToListAsync();

            return new PaginatedList<GuestBook>(parameter.PageIndex, parameter.PageSize, count, data);
        }

    }
}