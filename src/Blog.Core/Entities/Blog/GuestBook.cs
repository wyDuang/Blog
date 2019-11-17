using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Entities
{
    /// <summary>
    /// 留言表
    /// </summary>
    public class GuestBook : Entity
    {
        public Int32 ArticleId { get; set; }
        public Int32 ParentId { get; set; }
        public DateTime CreateDate { get; set; }
        public String UserName { get; set; }
        public String MobilePhone { get; set; }
        public String Email { get; set; }
        public sbyte IsDeleted { get; set; }
    }
}
