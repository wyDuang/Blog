using System;
using System.Collections.Generic;
using System.Text;

namespace WyBlog.Entities
{
    /// <summary>
    /// 分类表
    /// </summary>
    public class Category: BaseEntity
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// 分类展示名称
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 分类图标
        /// </summary>
        public string IconUrl { get; set; }
    }
}
