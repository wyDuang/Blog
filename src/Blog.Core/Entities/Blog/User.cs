using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Entities
{
    /// <summary>
    /// 用户信息表
    /// </summary>
    public class User : Entity
    {
        /// <summary>
		/// 用户名
		/// </summary>
		public String UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public String Password { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public String RealName { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public String MobilePhone { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public String Email { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public SByte Status { get; set; }
        /// <summary>
        /// 伪删除
        /// </summary>
        public SByte IsDeleted { get; set; }
        /// <summary>
        /// 角色Id
        /// </summary>
        public Int32 RoleId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }
        /// <summary>
        /// 最后登录IP
        /// </summary>
        public String LastLoginIp { get; set; }
        /// <summary>
        /// 错误次数
        /// </summary>
        public Int32 ErrorCount { get; set; }
    }
}
