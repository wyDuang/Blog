using Blog.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyContext _blogDbContext;

        public UnitOfWork(MyContext blogDbContext)
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
