using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Resources
{
    public class ArticleUpdateResource : ArticleAddOrUpdateResource
    {
        public int Id { get; set; }

        /// <summary>
        /// 伪删除
        /// </summary>
        public SByte IsDeleted { get; set; }
    }
}
