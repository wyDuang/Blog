using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using WYBlog.Entities;
using WYBlog.EntityFrameworkCore;
using WYBlog.IRepository;

namespace WYBlog.Repository
{
    public class GuestBookRepository : EfCoreRepository<BlogDbContext, GuestBook, int>, IGuestBookRepository
    {
        public GuestBookRepository(IDbContextProvider<BlogDbContext> dbContextProvider)
              : base(dbContextProvider)
        {
        }

        public async Task<List<GuestBook>> GetPagedListAsync(int skipCount, int maxResultCount, string sorting, int parentId, string filter = null)
        {
            return await DbSet
                .WhereIf(!filter.IsNullOrWhiteSpace(), x => x.Email.Contains(filter) && x.ParentId == parentId)
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}