using Blog.Core.BaseModels;
using Blog.Core.Entities;
using Blog.Core.Interfaces;
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
	/// <summary>
	/// 用户表（仓储层实现）
	/// </summary>
    public class UserRepository : EFRepository<User>, IUserRepository
    {
        public UserRepository(MyContext myContext) : base(myContext)
        {
        }

        public async Task<PaginatedList<User>> GetPageListAsync(UserParameter parameter, IPropertyMapping propertyMapping = null)
        {
			var query = Context.Set<User>().AsQueryable();

            //TODO: 条件


            if (propertyMapping != null)
            {
                query = query.ApplySort(parameter.OrderBy, propertyMapping);
            }

            var count = await query.CountAsync();
			var data = await query
                .Skip(parameter.PageIndex * parameter.PageSize)
                .Take(parameter.PageSize)
                .ToListAsync();

            return new PaginatedList<User>(parameter.PageIndex, parameter.PageSize, count, data);
        }

		public User GetUserByNameAndPwd(string userName, string password)
        {
            var query = Context.Set<User>().AsQueryable();

            return query.Where(x => x.UserName == userName && x.Password == password).FirstOrDefault();
        }
    }
}