using Blog.Core.BaseModels;
using Blog.Core.Entities;
using System.Threading.Tasks;

namespace Blog.Core.Interfaces
{
	/// <summary>
	/// 用户跟角色关联表（仓储层）
	/// </summary>
    public interface IUserRoleRepository : IRepositoryBase<UserRole>
    {
		Task<PaginatedList<UserRole>> GetPageListAsync(UserRoleParameter parameter, IPropertyMapping propertyMapping = null);
    }
}