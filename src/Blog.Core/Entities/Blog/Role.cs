using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Entities
{
	/// <summary>
	/// 角色表
	/// </summary>
	public class Role : Entity
	{
				/// <summary>
		/// 角色名
		/// </summary>
		public String RoleName { get; set; }
		/// <summary>
		/// 描述
		/// </summary>
		public String Description { get; set; }
		/// <summary>
		/// 排序
		/// </summary>
		public Int32 Sort { get; set; }
		/// <summary>
		/// 伪删除
		/// </summary>
		public SByte IsDeleted { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateDate { get; set; }

	}
}