using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Entities
{
    public class Article : Entity
    {
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
        /// 博客内容 Markdown
        /// </summary>
        public string Markdown { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public string Source { get; set; }
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
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; } = DateTime.Now;
        /// <summary>
        /// 博客标签
        /// </summary>
        //public virtual ICollection<Tag> ArticleTags { get; set; } = new List<Tag>();
    }
}
