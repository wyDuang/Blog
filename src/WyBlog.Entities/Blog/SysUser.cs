using System;
using System.Collections.Generic;
using System.Text;

namespace WyBlog.Entities
{
    /// <summary>
    /// 用户信息表
    /// </summary>
    public class SysUser : BaseEntity
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
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string MobilePhone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 最后登录时间 
        /// </summary>
        public DateTime LastLoginTime { get; set; }
        /// <summary>
        /// 最后登录时间 
        /// </summary>
        public string LastLoginIp { get; set; }
    }
}
