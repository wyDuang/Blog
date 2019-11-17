using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Entities
{
	/// <summary>
	/// 菜单与按钮关系表
	/// </summary>
	public class ModulePermission : Entity
	{
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