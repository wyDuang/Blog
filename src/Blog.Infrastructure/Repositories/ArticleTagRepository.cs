using Blog.Core.Entities;
using Blog.Core.Interfaces;
using Blog.Infrastructure.Database;

namespace Blog.Infrastructure.Repositories
{
    public class ArticleTagRepository : EFRepository<ArticleTag>, IArticleTagRepository
    {
        public ArticleTagRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
