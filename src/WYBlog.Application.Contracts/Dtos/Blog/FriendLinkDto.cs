using System;

namespace WYBlog.Dtos
{
    public class FriendLinkDto
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
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

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