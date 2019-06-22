using System;
using System.Collections.Generic;
using System.Text;

namespace WyBlog.Core.Models
{
    public class AppSettings
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 水印图片地址
        /// </summary>
        public string WatermarkPath { get; set; }
    }
}
