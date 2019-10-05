using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Entities
{
    public class Tag : Entity
    {
        /// <summary>
        /// 标签名称
        /// </summary>
        public string TagName { get; set; }
        /// <summary>
        /// 标签展示名称
        /// </summary>
        public string DisplayName { get; set; }
    }
}
