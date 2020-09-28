namespace WYBlog
{
    /// <summary>
    /// 缓存前缀
    /// </summary>
    public static class CachePrefixConsts
    {
        public const string Authorize = "Authorize";

        public const string Blog = "Blog";

        public const string Blog_Article = Blog + ":Article";

        public const string Blog_Tag = Blog + ":Tag";

        public const string Blog_Category = Blog + ":Category";

        public const string Blog_GuestBook = Blog + ":GuestBook";

        public const string Blog_FriendLink = Blog + ":FriendLink";
    }
}