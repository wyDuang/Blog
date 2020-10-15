namespace WYBlog
{
    public static class BlogDbTableConsts
    {
        #region Blog

        /// <summary>
        /// 文章表
        /// </summary>
        public const string Articles = "articles";

        /// <summary>
        /// 分类表
        /// </summary>
        public const string Categories = "categories";

        /// <summary>
        /// 标签表
        /// </summary>
        public const string Tags = "tags";

        /// <summary>
        /// 文章标签表
        /// </summary>
        public const string ArticleTags = "article_tags";

        /// <summary>
        /// 友情链接表
        /// </summary>
        public const string FriendLinks = "friendlinks";

        /// <summary>
        /// 留言表
        /// </summary>
        public const string GuestBooks = "guestBooks";

        // <summary>
        /// 广告表
        /// </summary>
        public const string Advertisements = "advertisement";

        #endregion Blog

        #region System
        // <summary>
        /// 用户表
        /// </summary>
        public const string SysUsers = "sys_users";

        // <summary>
        /// 角色表
        /// </summary>
        public const string SysRoles = "sys_roles";
        #endregion System

        /// <summary>
        /// 个性签名记录表
        /// </summary>
        public const string Signatures = "signatures";
    }
}