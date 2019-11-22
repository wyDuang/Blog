﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Resources
{
    public class ArticleAddOrUpdateResource
    {
        /// <summary>
		/// 文章分类
		/// </summary>
		public Int32 CategoryId { get; set; } = 1;

        /// <summary>
        /// 文章类型1-原，2-转，3-译
        /// </summary>
        public Int32 ArticleType { get; set; } = 1;

        /// <summary>
        /// 文章Key
        /// </summary>
        public String ArticleKey { get; set; } = "dasdadadasdasda";

        /// <summary>
        /// 标题
        /// </summary>
        public String Title { get; set; } = "dasdadadasdasda";

        /// <summary>
        /// 内容Html
        /// </summary>
        public String Html { get; set; } = "dasdadadasdasda";

        /// <summary>
        /// 内容Markdown
        /// </summary>
        public String Markdown { get; set; } = "dasdadadasdasda";

        /// <summary>
        /// 作者
        /// </summary>
        public String Author { get; set; } = "dasdadadasdasda";

        /// <summary>
        /// 备注
        /// </summary>
        public String Remark { get; set; } = "dasdadadasdasda";

        /// <summary>
        /// 链接
        /// </summary>
        public String LinkUrl { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public bool IsTop { get; set; } = false;

        /// <summary>
        /// 标签
        /// </summary>
        public int[] Tags { get; set; }
    }
}
