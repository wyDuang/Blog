using System;

namespace WyBlog.Core.Models
{
    public class JwtSettings
    {
        /// <summary>
        /// Token颁发机构
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// 颁发给谁
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// 安全密匙
        /// </summary>
        public string SecurityKey { get; set; }
        /// <summary>
        /// 过期时间（分钟）
        /// </summary>
        public int ExpireMinutes { get; set; }
    }
}
