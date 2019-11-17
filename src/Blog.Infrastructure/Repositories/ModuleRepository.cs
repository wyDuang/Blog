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
	/// 接口API地址信息表（仓储层实现）
	/// </summary>
    public class ModuleRepository : EFRepository<Module>, IModuleRepository
    {
        public ModuleRepository(MyContext myContext) : base(myContext)
        {
        }

        public async Task<PaginatedList<Module>> GetPageListAsync(ModuleParameter parameter, IPropertyMapping propertyMapping = null)
        {
			var query = dbSet.AsQueryable();

            //TODO:  v 条件

            if (propertyMapping != null)
            {
                query = query.ApplySort(parameter.OrderBy, propertyMapping);
            }

            var count = await query.CountAsync();
			var data = await query
                .Skip(parameter.PageIndex * parameter.PageSize)
                .Take(parameter.PageSize)
                .ToListAsync();

            return new PaginatedList<Module>(parameter.PageIndex, parameter.PageSize, count, data);
        }
    }
}