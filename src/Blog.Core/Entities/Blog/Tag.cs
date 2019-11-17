using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Entities
{
    /// <summary>
	/// 标签表
	/// </summary>
    public class Tag : Entity
    {
        /// <summary>
        /// 标签Key
        /// </summary>
        public String TagKey { get; set; }
        /// <summary>
        /// 标签名
        /// </summary>
        public String TagName { get; set; }
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
