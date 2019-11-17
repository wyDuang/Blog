using System.Threading.Tasks;
using Blog.Core.Entities;

namespace Blog.Core.Interfaces
{
	/// <summary>
	/// 菜单与按钮关系表（仓储层）
	/// </summary>
    public interface IModulePermissionRepository : IRepositoryBase<ModulePermission>
    {

    }
}