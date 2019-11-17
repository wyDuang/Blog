using System.Linq;
using System.Threading.Tasks;
using Blog.Core.BaseModels;
using Blog.Core.Entities;
using Blog.Core.Interfaces;
using Blog.Infrastructure.Database;
using Blog.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Repositories
{
    /// <summary>
    /// 广告表（仓储层实现）
    /// </summary>
    public class AdvertisementRepository : EFRepository<Advertisement>, IAdvertisementRepository
    {
        public AdvertisementRepository(MyContext myContext) : base(myContext)
        {
        }

        public async Task<PaginatedList<Advertisement>> GetPageListAsync(AdvertisementParameter parameter, IPropertyMapping propertyMapping = null)
        {
			var query = Context.Set<Advertisement>().AsQueryable();

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

            return new PaginatedList<Advertisement>(parameter.PageIndex, parameter.PageSize, count, data);
        }

    }
}