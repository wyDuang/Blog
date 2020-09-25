using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace WYBlog.Configurations
{
    /// <summary>
    /// appsettings.json配置文件数据读取类
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// 配置文件的根节点
        /// </summary>
        private static readonly IConfigurationRoot _config;

        /// <summary>
        /// Constructor
        /// </summary>
        static AppSettings()
        {
            // 加载appsettings.json，并构建IConfigurationRoot
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true);

            _config = builder.Build();
        }

        /// <summary>
        /// ApiVersion
        /// </summary>
        public static string ApiVersion => _config["App:ApiVersion"];

        /// <summary>
        /// 允许跨域访问地址
        /// </summary>
        public static string CorsOrigins => _config["App:CorsOrigins"];

        /// <summary>
        /// JwtAuth
        /// </summary>
        public static class JwtAuth
        {
            public static string Audience => _config["JwtAuth:Audience"];

            public static string Issuer => _config["JwtAuth:Issuer"];

            public static string SecurityKey => _config["JwtAuth:SecurityKey"];

            public static int Expires => Convert.ToInt32(_config["JWT:Expires"]);
        }
    }
}
