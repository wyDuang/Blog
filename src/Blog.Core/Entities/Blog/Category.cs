using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Entities
{
    /// <summary>
    /// 分类表
    /// </summary>
    public class Category: Entity
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
        /// url的key值
        /// </summary>
        public string CategoryKey { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}
