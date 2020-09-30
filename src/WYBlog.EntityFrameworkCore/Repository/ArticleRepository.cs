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
    public class ArticleRepository : EfCoreRepository<BlogDbContext, Article, int>, IArticleRepository
    {
        public ArticleRepository(IDbContextProvider<BlogDbContext> dbContextProvider)
           : base(dbContextProvider)
        {
        }

        public async Task<Article> FindByKeyAsync(string key)
        {
            return await DbSet.FirstOrDefaultAsync(x => x.ArticleKey == key);
        }

        public async Task<Article> FindByTitleAsync(string title)
        {
            return await DbSet.FirstOrDefaultAsync(x => x.Title == title);
        }

        public async Task<List<Article>> GetPagedListAsync(int skipCount, int maxResultCount, string sorting, string filter = null)
        {
            return await DbSet
                .WhereIf(!filter.IsNullOrWhiteSpace(), x => x.Title.Contains(filter))
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}