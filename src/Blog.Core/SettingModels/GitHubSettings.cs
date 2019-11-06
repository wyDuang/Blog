using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.SettingModels
{
    /// <summary>
    /// GitHub配置文件
    /// 接入地址：https://developer.github.com/apps/building-oauth-apps/authorizing-oauth-apps/
    /// </summary>
    public class GitHubSettings
    {
        /// <summary>
        /// GET请求，跳转GitHub登录界面，获取用户授权，得到code
        /// </summary>
        public string API_Authorize { get; set; }

        /// <summary>
        /// POST请求，根据code得到access_token
        /// </summary>
        public string API_AccessToken { get; set; }

        /// <summary>
        /// GET请求，根据access_token得到用户信息
        /// </summary>
        public string API_User { get; set; }

        /// <summary>
        /// Application name
        /// </summary>
        public string ApplicationName { get; set; }

        /// <summary>
        /// Client ID
        /// </summary>
        public string Client_Id { get; set; }

        /// <summary>
        /// Client Secret
        /// </summary>
        public string Client_Secret { get; set; }

        /// <summary>
        /// Authorization callback URL
        /// </summary>
        public string Redirect_Uri { get; set; }
    }
}
