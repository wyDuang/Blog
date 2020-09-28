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
    public class TagRepository : EfCoreRepository<BlogDbContext, Tag, int>, ITagRepository
    {
        public TagRepository(IDbContextProvider<BlogDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<List<Tag>> GetPagedListAsync(int skipCount, int maxResultCount, string sorting, string filter = null)
        {
            return await DbSet
                .WhereIf(!filter.IsNullOrWhiteSpace(), x => x.TagName.Contains(filter))
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }

        public async Task<Tag> FindByNameAsync(string name)
        {
            return await DbSet.FirstOrDefaultAsync(x => x.TagName == name);
        }

        public async Task<Tag> FindByKeyAsync(string key)
        {
            return await DbSet.FirstOrDefaultAsync(x => x.TagKey == key);
        }
    }
}
