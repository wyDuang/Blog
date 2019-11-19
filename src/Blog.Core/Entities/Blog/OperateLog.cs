using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Entities
{
	/// <summary>
	/// 操作日志表
	/// </summary>
	public class OperateLog : Entity
	{
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
		/// IP地址
		/// </summary>
		public String IpAddress { get; set; }
		public String Description { get; set; }
		/// <summary>
		/// 登录名称
		/// </summary>
		public String LoginUserName { get; set; }
		/// <summary>
		/// 登录ID
		/// </summary>
		public Int32 LoginUserId { get; set; }
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