using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Resources
{
    public class ArticleResource
    {
        public int Id { get; set; }

        /// <summary>
        /// 博客标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 博客类别
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// 博客内容 HTML
        /// </summary>
        public string Html { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// 点击数量
        /// </summary>
        public int ClickCount { get; set; }
        /// <summary>
        /// 评论数量
        /// </summary>
        public int CommentCount { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
    }
}
