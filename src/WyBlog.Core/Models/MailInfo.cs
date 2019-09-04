using System;
using System.Collections.Generic;
using System.Text;
using WyBlog.Core.Models.SettingModel;

namespace WyBlog.Core.Models
{
    public class MailInfo
    {
        /// <summary>
        /// 收件人邮箱
        /// </summary>
        public string ToAddress { get; set; }
        /// <summary>
        /// 主题
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 邮件内容
        /// </summary>
        public string Bodys { get; set; }

        public EmailConfig EmailConfig { get; set; }
    }
}
