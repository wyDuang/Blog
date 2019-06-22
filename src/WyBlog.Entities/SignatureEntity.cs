using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WyBlog.Entities
{
    public enum SignatureEnum
    {
        [Display(Name = "签名字体"), Description("签名字体")]
        _qmzt = 6,
        [Display(Name = "明星手写体"), Description("明星手写体")]
        _mxsxt = 5,
        [Display(Name = "一笔艺术签"), Description("一笔艺术签")]
        _ybysq = 901,
        [Display(Name = "连笔商务签"), Description("连笔商务签")]
        _lbswq = 904,
        [Display(Name = "一笔商务签"), Description("一笔商务签")]
        _ybswq = 905,
    }

    /// <summary>
    /// 签名实体
    /// </summary>
    public class SignatureEntity
    {
        /// <summary>
        /// 签名姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 签名类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 返回签名地址
        /// </summary>
        public string Url { get; set; }
    }
}
