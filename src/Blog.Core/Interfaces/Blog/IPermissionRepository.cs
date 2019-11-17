using System.Threading.Tasks;
using Blog.Core.BaseModels;
using Blog.Core.Entities;

namespace Blog.Core.Interfaces
{
	/// <summary>
	/// 菜单表（仓储层）
	/// </summary>
    public interface IPermissionRepository : IRepositoryBase<Permission>
    {
		Task<PaginatedList<Permission>> GetPageListAsync(PermissionParameter parameter, IPropertyMapping propertyMapping = null);
    }
}