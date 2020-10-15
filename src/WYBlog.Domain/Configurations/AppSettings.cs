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