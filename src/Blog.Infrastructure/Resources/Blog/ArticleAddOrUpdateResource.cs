﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Resources
{
    public class ArticleAddOrUpdateResource
    {
        /// <summary>
        /// 博客key
        /// </summary>
        public string ArticleKey { get; set; }
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
    }
}