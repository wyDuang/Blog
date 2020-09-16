using Volo.Abp.Domain.Entities;

namespace WYBlog.Entities
{
    public class ArticleTag : Entity<int>
    {
        /// <summary>
        /// 文章Id
        /// </summary>
        public int ArticleId { get; set; }

        /// <summary>
        /// 标签Id
        /// </summary>
        public int TagId { get; set; }
    }
}