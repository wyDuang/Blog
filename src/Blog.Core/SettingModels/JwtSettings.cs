using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.SettingModels
{
    public class JwtSettings
    {
        /// <summary>
        /// Token发布者
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// Token接受者
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// 签名秘钥
        /// </summary>
        public string IssuerSigningKey { get; set; }
        /// <summary>
        /// 访问令牌过期分钟数
        /// </summary>
        public int TokenExpiresMinutes { get; set; }
        /// <summary>
        /// 刷新令牌接受者
        /// </summary>
        public string RefreshTokenAudience { get; set; }
        /// <summary>
        /// 刷新令牌过期分钟数
        /// </summary>
        public int RefreshTokenExpiresMinutes { get; set; }
    }
}
