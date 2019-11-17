using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Entities
{
    /// <summary>
	/// 文章标签表
	/// </summary>
    public class ArticleTag : Entity
    {
        /// <summary>
        /// 文章Id
        /// </summary>
        public int ArticleId { get; set; }
        /// <summary>
        /// 标签Id
        /// </summary>
        public int TagId { get; set; }
        /// <summary>
        /// 创建时间
		/// </summary>
		public DateTime CreateDate { get; set; }
    }
}
