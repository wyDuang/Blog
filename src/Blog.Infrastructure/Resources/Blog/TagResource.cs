using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Resources
{
    public class TagResource
    {
        public int Id { get; set; }
        /// <summary>
        /// 标签Key
        /// </summary>
        public string TagKey { get; set; }
        /// <summary>
        /// 标签名称
        /// </summary>
        public string TagName { get; set; }
    }
}
