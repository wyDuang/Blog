using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Entities
{
    /// <summary>
	/// 广告表
	/// </summary>
    public class Advertisement : Entity
    {
        /// <summary>
		/// 广告图片
		/// </summary>
		public String ImgUrl { get; set; }
        /// <summary>
        /// 广告标题
        /// </summary>
        public String Title { get; set; }
        /// <summary>
        /// 广告链接
        /// </summary>
        public String LinkUrl { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public String Remark { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 伪删除
        /// </summary>
        public SByte IsDeleted { get; set; }
    }
}
