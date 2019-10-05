using Blog.Core.Entities;
using Blog.Core.Interfaces;
using Blog.Infrastructure.Database;

namespace Blog.Infrastructure.Repositories
{
    public class TagRepository : EFRepository<Tag>, ITagRepository
    {
        public TagRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
