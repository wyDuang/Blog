using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WyBlog.Models
{
    public class Tag : BaseModel
    {
        /// <summary>
        /// 标签名称
        /// </summary>
        public string TagName { get; set; }
        /// <summary>
        /// 标签展示名称
        /// </summary>
        public string DisplayName { get; set; }
    }
}
