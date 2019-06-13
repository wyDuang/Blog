using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Entities
{
    public class ArticleTag : Entity
    {
        /// <summary>
        /// 文章Id
        /// </summary>
        public int ArticleId { get; set; }
        /// <summary>
        /// 标签Id
        /// </summary>
        public int TagId { get; set; }
    }
}
