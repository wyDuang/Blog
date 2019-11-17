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
	/// <summary>
	/// 角色表（仓储层实现）
	/// </summary>
    public class RoleRepository : EFRepository<Role>, IRoleRepository
    {
        public RoleRepository(MyContext myContext) : base(myContext)
        {
        }

        public async Task<PaginatedList<Role>> GetPageListAsync(RoleParameter parameter, IPropertyMapping propertyMapping = null)
        {
			var query = dbSet.AsQueryable();

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

            return new PaginatedList<Role>(parameter.PageIndex, parameter.PageSize, count, data);
        }
    }
}