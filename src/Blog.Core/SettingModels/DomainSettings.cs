using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.SettingModels
{
    public class DomainSettings
    {
        public List<SubDomain> SubDomains = new List<SubDomain>();
    }

    /// <summary>
    /// 域名信息实体类
    /// </summary>
    public class SubDomain
    {
        /// 域名
        /// <summary>
        /// 域名
        /// </summary>
        public string Domain { get; set; }

        /// 区域名
        /// <summary>
        /// 区域名
        /// </summary>
        public string AreaName { get; set; }

        /// 控制器名
        /// <summary>
        /// 控制器名
        /// </summary>
        public string Controller { get; set; }
    }
}
