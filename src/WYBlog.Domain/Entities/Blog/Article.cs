﻿using System;
using Volo.Abp.Domain.Entities;

namespace WYBlog.Entities
{
    /// <summary>
    /// 文章表
    /// </summary>
    public class Article : Entity<int>
    {
        /// <summary>
		/// 文章分类
		/// </summary>
		public int CategoryId { get; set; }

        /// <summary>
        /// 文章类型
        /// </summary>
        public sbyte ArticleType { get; set; }

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
        /// 伪删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public bool IsTop { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}