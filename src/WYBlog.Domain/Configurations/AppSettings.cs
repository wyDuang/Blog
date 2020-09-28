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

            public static int Expires => Convert.ToInt32(_config["JwtAuth:Expires"]);
        }

        /// <summary>
        /// Caching
        /// </summary>
        public static class Caching
        {
            /// <summary>
            /// RedisConnectionString
            /// </summary>
            public static string RedisConnectionString => _config["Caching:RedisConnectionString"];

            /// <summary>
            /// 是否开启
            /// </summary>
            public static bool IsOpen => Convert.ToBoolean(_config["Caching:IsOpen"]);
        }

        /// <summary>
        /// RabbitMQ
        /// </summary>
        public static class RabbitMQ
        {
            public static class Connections
            {
                public static class Default
                {
                    public static string Username => _config["RabbitMQ:Connections:Default:Username"];

                    public static string Password => _config["RabbitMQ:Connections:Default:Password"];

                    public static string HostName => _config["RabbitMQ:Connections:Default:HostName"];

                    public static int Port => Convert.ToInt32(_config["RabbitMQ:Connections:Default:Port"]);
                }
            }

            public static class EventBus
            {
                public static string ClientName => _config["RabbitMQ:EventBus:ClientName"];

                public static string ExchangeName => _config["RabbitMQ:EventBus:ExchangeName"];
            }
        }

        /// <summary>
        /// GitHub
        /// </summary>
        public static class GitHub
        {
            public static int UserId => Convert.ToInt32(_config["Github:UserId"]);

            public static string Client_ID => _config["Github:ClientID"];

            public static string Client_Secret => _config["Github:ClientSecret"];

            public static string Redirect_Uri => _config["Github:RedirectUri"];

            public static string ApplicationName => _config["Github:ApplicationName"];

            public static string Scope => _config["Github:Scope"];
        }

        ///// <summary>
        ///// UploadConfig
        ///// </summary>
        //public static class UploadConfig
        //{
        //    public static string UploadFile => _config["UploadConfig:UploadFile"];
        //    public static string Domain => _config["UploadConfig:Domain"];
        //}
    }
}
