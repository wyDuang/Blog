using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Entities
{
    /// <summary>
    /// 分类表
    /// </summary>
    public class Category: Entity
    {
        /// <summary>
		/// key值
		/// </summary>
		public String CategoryKey { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public String CategoryName { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public Int32 Sort { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public String Remark { get; set; }
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
