using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WYBlog.Enums
{
    public enum SignatureEnum
    {
        [Display(Name = "签名字体"), Description("签名字体")]
        qmzt = 6,
        [Display(Name = "明星手写体"), Description("明星手写体")]
        mxsxt = 5,
        [Display(Name = "一笔艺术签"), Description("一笔艺术签")]
        ybysq = 901,
        [Display(Name = "连笔商务签"), Description("连笔商务签")]
        lbswq = 904,
        [Display(Name = "一笔商务签"), Description("一笔商务签")]
        ybswq = 905,
    }
}
