using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Entities
{
	/// <summary>
	/// 菜单表
	/// </summary>
	public class Permission : Entity
	{
		/// <summary>
		/// 菜单执行Action名
		/// </summary>
		public String PermissionCode { get; set; }
		/// <summary>
		/// 菜单显示名（如用户页、编辑(按钮)、删除(按钮)）
		/// </summary>
		public String PermissionName { get; set; }
		/// <summary>
		/// 是否是按钮
		/// </summary>
		public SByte IsButton { get; set; }
		/// <summary>
		/// 是否是隐藏菜单
		/// </summary>
		public SByte IsHide { get; set; }
		/// <summary>
		/// 按钮事件
		/// </summary>
		public String Func { get; set; }
		/// <summary>
		/// 排序
		/// </summary>
		public Int32 Sort { get; set; }
		/// <summary>
		/// 菜单图标
		/// </summary>
		public String Icon { get; set; }
		/// <summary>
		/// 伪删除
		/// </summary>
		public SByte IsDeleted { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateDate { get; set; }
		/// <summary>
		/// 激活状态
		/// </summary>
		public SByte IsEnabled { get; set; }

	}
}