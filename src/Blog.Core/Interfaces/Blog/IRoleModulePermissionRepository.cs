using Blog.Core.Entities;

namespace Blog.Core.Interfaces
{
    /// <summary>
    /// 按钮跟权限关联表（仓储层）
    /// </summary>
    public interface IRoleModulePermissionRepository : IRepositoryBase<RoleModulePermission>
    {
    }
}