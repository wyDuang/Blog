using System;
using System.Collections.Generic;
using System.Text;
using WYBlog.Enums;

namespace WYBlog
{
    /// <summary>
    /// 返回结果基类 
    /// </summary>
    public class BaseResult
    {
        /// <summary>
        /// 结果编码
        /// </summary>
        public ResultCodeEnum Code { get; set; } = ResultCodeEnum.Succeed;
        /// <summary>
        /// 结果消息 如果不成功，返回的错误信息
        /// </summary>
        public string Msg { get; set; } = ResultMessageConsts.SUCCESS;

        /// <summary>
        /// 时间戳(毫秒)
        /// </summary>
        public long Timestamp { get; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

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
        public BaseResult(ResultCodeEnum code, string msg)
        {
            Code = code;
            Msg = msg;
        }
    }
}
