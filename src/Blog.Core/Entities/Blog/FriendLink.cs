using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Entities
{
    /// <summary>
	/// 友情链接表
	/// </summary>
    public class FriendLink: Entity
    {
        /// <summary>
		/// 标题
		/// </summary>
		public String Title { get; set; }
        /// <summary>
        /// 链接
        /// </summary>
        public String LinkUrl { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public String Avatar { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public String Description { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; } = DateTime.Now;
        /// <summary>
        /// 伪删除
        /// </summary>
        public SByte IsDeleted { get; set; }
    }
}
