using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using WYBlog.Entities;
using WYBlog.EntityFrameworkCore;
using WYBlog.IRepository;

namespace WYBlog.Repository
{
    public class CategoryRepository : EfCoreRepository<BlogDbContext, Category, int>, ICategoryRepository
    {
        public CategoryRepository(IDbContextProvider<BlogDbContext> dbContextProvider)
           : base(dbContextProvider)
        {
        }

        public async Task<Category> FindByKeyAsync(string key)
        {
            return await DbSet.FirstOrDefaultAsync(x => x.CategoryKey == key);
        }

        public async Task<Category> FindByNameAsync(string name)
        {
            return await DbSet.FirstOrDefaultAsync(x => x.CategoryName == name);
        }

        public async Task<List<Category>> GetPagedListAsync(int skipCount, int maxResultCount, string sorting, string filter = null)
        {
            return await DbSet
                .WhereIf(!filter.IsNullOrWhiteSpace(), x => x.CategoryName.Contains(filter))
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}