using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace WYBlog.Entities
{
    /// <summary>
    /// 签名实体表
    /// </summary>
    public class Signature : Entity<Guid>
    {
        /// <summary>
        /// 签名姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 签名类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 返回签名地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// IP
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}
