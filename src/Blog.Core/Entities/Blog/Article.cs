using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Entities
{
    /// <summary>
	/// 文章表
	/// </summary>
    public class Article : Entity
    {
        /// <summary>
		/// 文章分类
		/// </summary>
		public Int32 CategoryId { get; set; }
        /// <summary>
        /// 文章类型1-原，2-转，3-译
        /// </summary>
        public SByte ArticleType { get; set; }
        /// <summary>
        /// 文章Key
        /// </summary>
        public String ArticleKey { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public String Title { get; set; }
        /// <summary>
        /// 内容Html
        /// </summary>
        public String Html { get; set; }
        /// <summary>
        /// 内容Markdown
        /// </summary>
        public String Markdown { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public String Author { get; set; }
        /// <summary>
        /// 链接
        /// </summary>
        public String LinkUrl { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public String Remark { get; set; }
        /// <summary>
        /// 点击数量
        /// </summary>
        public Int32 ClickCount { get; set; }
        /// <summary>
        /// 评论数量
        /// </summary>
        public Int32 CommentCount { get; set; }
        /// <summary>
        /// 伪删除
        /// </summary>
        public SByte IsDeleted { get; set; }
        /// <summary>
        /// 是否置顶
        /// </summary>
        public SByte IsTop { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; } = DateTime.Now;
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateDate { get; set; } = DateTime.Now;
    }
}
