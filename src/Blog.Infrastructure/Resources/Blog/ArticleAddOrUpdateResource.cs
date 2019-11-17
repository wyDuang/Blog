using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Resources
{
    public class ArticleAddOrUpdateResource
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
        /// 备注
        /// </summary>
        public String Remark { get; set; }

        /// <summary>
        /// 伪删除
        /// </summary>
        public SByte IsDeleted { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public SByte IsTop { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public int[] Tags { get; set; }
    }
}
