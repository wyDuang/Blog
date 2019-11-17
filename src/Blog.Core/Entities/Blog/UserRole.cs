using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Entities
{
	/// <summary>
	/// 用户跟角色关联表
	/// </summary>
	public class UserRole : Entity
	{
				/// <summary>
		/// 用户ID
		/// </summary>
		public Int32 UserId { get; set; }
		/// <summary>
		/// 角色ID
		/// </summary>
		public Int32 RoleId { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateDate { get; set; }

	}
}