using System;
using System.Collections.Generic;
using System.Text;

namespace WyBlog.Core.Models
{
    /// <summary>
    /// 返回结果基类 
    /// </summary>
    public class BaseResult
    {
        /// <summary>
        /// 结果编码
        /// </summary>
        public int Code { get; set; } = 0;
        /// <summary>
        /// 结果消息 如果不成功，返回的错误信息
        /// </summary>
        public string Msg { get; set; } = "success";

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public BaseResult()
        {
        }

        /// <summary>
        /// 有参构造函数
        /// </summary>
        /// <param name="Code">结果代码</param>
        /// <param name="Msg">结果信息</param>
        public BaseResult(int code, string msg)
        {
            Code = code;
            Msg = msg;
        }
    }
}
