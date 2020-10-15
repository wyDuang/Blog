namespace WYBlog.Configurations
{
    /// <summary>
    /// Redis 缓存
    /// </summary>
    public class CacheConfig
    {
        /// <summary>
        /// RedisConnectionString
        /// </summary>
        public string RedisConnectionString { get; set; }

        /// <summary>
        /// 是否开启
        /// </summary>
        public bool IsOpen { get; set; }
    }
}