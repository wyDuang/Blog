using System;
using Volo.Abp.Domain.Entities;

namespace WYBlog.Entities
{
    /// <summary>
    /// 留言表
    /// </summary>
    public class GuestBook : Entity<int>
    {
        /// <summary>
        /// 文章Id
        /// </summary>
        public int ArticleId { get; set; }

        /// <summary>
        /// 父Id
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string MobilePhone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 伪删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}