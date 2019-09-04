using System;
using System.Collections.Generic;
using System.Text;

namespace WyBlog.Core.Models.SettingModel
{
    public class EmailConfig
    {
        /// <summary>
        /// 邮件主机地址
        /// </summary>
        public string Host { get; set; }

        public int Port { get; set; }

        public bool EnableSsl { get; set; }

        public string UserName { get; set; }

        /// <summary>
        /// 发件人邮箱
        /// </summary>
        public string Address { get; set; }

        public string Password { get; set; }
    }
}
