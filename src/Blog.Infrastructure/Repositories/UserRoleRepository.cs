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
	/// 用户跟角色关联表（仓储层实现）
	/// </summary>
    public class UserRoleRepository : EFRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(MyContext myContext) : base(myContext)
        {
        }

        public async Task<PaginatedList<UserRole>> GetPageListAsync(UserRoleParameter parameter, IPropertyMapping propertyMapping = null)
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

            return new PaginatedList<UserRole>(parameter.PageIndex, parameter.PageSize, count, data);
        }
    }
}