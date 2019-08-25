﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WyBlog.Entities
{
    public class FriendLink: BaseEntity
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        public string LinkUrl { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; } = DateTime.Now;

        //public string Avatar { get; set; }
    }
}