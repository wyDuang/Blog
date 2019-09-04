using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WyBlog.IRepository;
using WyBlog.Repository.MySql.Database;

namespace WyBlog.Repository.MySql.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BlogDbContext _blogDbContext;
        public UnitOfWork(
            BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext;
        }

        public bool Save()
        {
            return _blogDbContext.SaveChanges() > 0;
        }

        public async Task<bool> SaveAsync()
        {
            return await _blogDbContext.SaveChangesAsync() > 0;
        }
    }
}
