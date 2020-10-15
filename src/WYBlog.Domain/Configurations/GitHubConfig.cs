namespace WYBlog.Configurations
{
    public class GitHubConfig
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

        public int UserId { get; set; }

        public string Client_ID { get; set; }

        public string Client_Secret { get; set; }

        public string Redirect_Uri { get; set; }

        public string ApplicationName { get; set; }

        public string Scope { get; set; }
    }
}