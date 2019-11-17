using Blog.Core.BaseModels;
using Blog.Core.Entities;
using Blog.Core.Interfaces;
using Blog.Core.QueryParameters;
using Blog.Infrastructure.Database;
using Blog.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Repositories
{
    public class FriendLinkRepository : EFRepository<FriendLink>, IFriendLinkRepository
    {
        public FriendLinkRepository(MyContext myContext) : base(myContext)
        {
        }

        public async Task<PaginatedList<FriendLink>> GetPageListAsync(FriendLinkParameter parameter, IPropertyMapping propertyMapping = null)
        {
            var query = dbSet.AsQueryable();

            if (!string.IsNullOrEmpty(parameter.Title))
            {
                var title = parameter.Title.ToLowerInvariant();
                query = query.Where(x => x.Title.ToLowerInvariant() == title);
            }

            if (propertyMapping != null)
            {
                query = query.ApplySort(parameter.OrderBy, propertyMapping);
            }

            var count = await query.CountAsync();
            var data = await query
                .Skip(parameter.PageIndex * parameter.PageSize)
                .Take(parameter.PageSize)
                .ToListAsync();

            return new PaginatedList<FriendLink>(parameter.PageIndex, parameter.PageSize, count, data);
        }
    }
}
