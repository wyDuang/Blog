using Volo.Abp.Domain.Entities;

namespace WYBlog.Entities
{
    /// <summary>
    /// 标签表
    /// </summary>
    public class Tag : Entity<int>
    {
        /// <summary>
        /// 标签名称
        /// </summary>
        public string TagName { get; set; }

        /// <summary>
        /// 标签Key
        /// </summary>
        public string TagKey { get; set; }
    }
}