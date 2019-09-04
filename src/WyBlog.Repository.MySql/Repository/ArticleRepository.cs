using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WyBlog.Core.AutoMapper;
using WyBlog.Core.Models;
using WyBlog.Entities;
using WyBlog.IRepository;
using WyBlog.Repository.MySql.Database;

namespace WyBlog.Repository.MySql.Repository
{
    public class ArticleRepository : BaseRepository<Article, int>, IArticleRepository
    {
        private readonly IPropertyMappingContainer _propertyMappingContainer;
        public ArticleRepository(
            BlogDbContext dbContext,
            IPropertyMappingContainer propertyMappingContainer)
            : base(dbContext)
        {
            _propertyMappingContainer = propertyMappingContainer;
        }

        public Task<PaginatedList<Article>> GetPageListAsync(PaginatedQuery queryParameters)
        {
            throw new NotImplementedException();
        }
    }
}
