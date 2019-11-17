using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Entities
{
    /// <summary>
    /// 接口API地址信息表
    /// </summary>
    public class Module : Entity
	{
		/// <summary>
		/// 父ID
		/// </summary>
		public Int32 ParentId { get; set; }
		/// <summary>
		/// 菜单编号
		/// </summary>
		public String ModuleCode { get; set; }
		/// <summary>
		/// 名称
		/// </summary>
		public String ModuleName { get; set; }
		/// <summary>
		/// 菜单链接地址
		/// </summary>
		public String LinkUrl { get; set; }
		/// <summary>
		/// 描述
		/// </summary>
		public String Description { get; set; }
		/// <summary>
		/// 区域名称
		/// </summary>
		public String Area { get; set; }
		/// <summary>
		/// 控制器名称
		/// </summary>
		public String Controller { get; set; }
		/// <summary>
		/// Action名称
		/// </summary>
		public String Action { get; set; }
		/// <summary>
		/// 图标
		/// </summary>
		public String Icon { get; set; }
		/// <summary>
		/// 排序
		/// </summary>
		public Int32 Sort { get; set; }
		/// <summary>
		/// 是否是右侧菜单
		/// </summary>
		public SByte IsMenu { get; set; }
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