using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Resources
{
    public class CategoryUpdateResource: CategoryAddOrUpdateResource
    {
        /// <summary>
        /// url的key值
        /// </summary>
        public string CategoryKey { get; set; }
    }
}
