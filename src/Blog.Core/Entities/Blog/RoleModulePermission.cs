using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Entities
{
	/// <summary>
	/// 按钮跟权限关联表
	/// </summary>
	public class RoleModulePermission : Entity
	{
				/// <summary>
		/// 角色ID
		/// </summary>
		public Int32 RoleId { get; set; }
		/// <summary>
		/// 菜单ID
		/// </summary>
		public Int32 ModuleId { get; set; }
		/// <summary>
		/// 按钮ID
		/// </summary>
		public Int32 PermissionId { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateDate { get; set; }

	}
}