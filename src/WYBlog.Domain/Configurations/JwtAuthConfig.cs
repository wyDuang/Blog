using System;
using System.Collections.Generic;
using System.Text;

namespace WYBlog.Configurations
{
    /// <summary>
    /// JwtAuth
    /// </summary>
    public class JwtAuthConfig
    {
        /// <summary>
        /// 接受者
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 签发者
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 密匙
        /// </summary>
        public string SecurityKey { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public int Expires { get; set; }
    }
}
