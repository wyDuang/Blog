
using Blog.Core.Entities;
using Blog.Core.Interfaces;
using Blog.Infrastructure.Database;

namespace Blog.Infrastructure.Repositories
{
    public class CategoryRepository : EFRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
