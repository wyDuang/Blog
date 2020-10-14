using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;

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

                    public static int Port => _config.GetSection("RabbitMQ:Connections:Default:Port").Get<int>();
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
            /// <summary>
            /// GET请求，跳转GitHub登录界面，获取用户授权，得到code
            /// </summary>
            public static string API_Authorize => _config["Github:API_Authorize"];

            /// <summary>
            /// POST请求，根据code得到access_token
            /// </summary>
            public static string API_AccessToken => _config["Github:API_AccessToken"];

            /// <summary>
            /// GET请求，根据access_token得到用户信息
            /// </summary>
            public static string API_User => _config["Github:API_User"];

            public static int UserId => _config.GetSection("Github:UserId").Get<int>();

            public static string Client_ID => _config["Github:ClientID"];

            public static string Client_Secret => _config["Github:ClientSecret"];

            public static string Redirect_Uri => _config["Github:RedirectUri"];

            public static string ApplicationName => _config["Github:ApplicationName"];

            public static string Scope => _config["Github:Scope"];
        }

        /// <summary>
        /// Hangfire
        /// </summary>
        public static class Hangfire
        {
            public static string ConnectionString => _config["Hangfire:ConnectionString"];

            public static string Login => _config["Hangfire:Login"];
            
            public static string Password => _config["Hangfire:Password"];
        }

        /// <summary>
        /// FileUpload
        /// </summary>
        public static class FileUpload
        {
            /// <summary>
            /// 文件上传目录
            /// </summary>
            public static string FileUploadLocalFolder => _config["UploadFile:FileUploadLocalFolder"];

            /// <summary>
            /// 允许的文件最大大小
            /// </summary>
            public static int MaxFileSize => _config.GetSection("UploadFile:MaxFileSize ").Get<int>();

            /// <summary>
            /// 允许的文件类型
            /// </summary>
            public static string[] AllowedUploadFormats => _config.GetSection("UploadFile:MaxFileSize").Get<string[]>();
        }
    }
}