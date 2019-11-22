using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Resources
{
    public class ArticleResource
    {
        /// <summary>
        /// 文章类型1-原，2-转，3-译
        /// </summary>
        public Int32 ArticleType { get; set; }
        /// <summary>
        /// 博客key
        /// </summary>
        public string ArticleKey { get; set; }
        /// <summary>
        /// 博客标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 博客内容 HTML
        /// </summary>
        public string Html { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 链接
        /// </summary>
        public string LinkUrl { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public bool IsTop { get; set; }

        /// <summary>
        /// 博客类别Key
        /// </summary>
        public string CategoryName { get; set; }

        public string[] Tags { get; set; }
    }
}
