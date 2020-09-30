using System;
using System.Collections.Generic;
using System.Text;
using WYBlog.Enums;

namespace WYBlog.Dtos
{
    public class CreateOrEditArticleDto
    {
        /// <summary>
		/// 文章分类
		/// </summary>
		public int CategoryId { get; set; }

        /// <summary>
        /// 文章类型
        /// </summary>
        public ArticleTypeEnum ArticleType { get; set; }

        /// <summary>
        /// 文章Key
        /// </summary>
        public string ArticleKey { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容Html
        /// </summary>
        public string Html { get; set; }

        /// <summary>
        /// 内容Markdown
        /// </summary>
        public string Markdown { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        public string LinkUrl { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 点击数量
        /// </summary>
        public int ClickCount { get; set; }

        /// <summary>
        /// 评论数量
        /// </summary>
        public int CommentCount { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public bool IsTop { get; set; }
    }
}
