using System;

namespace WyBlog.Authentication
{
    public class JwtSettings
    {
        /// <summary>
        /// 
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// 
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
