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
	/// 操作日志表（仓储层实现）
	/// </summary>
    public class OperatelogRepository : EFRepository<OperateLog>, IOperateLogRepository
    {
        public OperatelogRepository(MyContext myContext) : base(myContext)
        {
        }

        public async Task<PaginatedList<OperateLog>> GetPageListAsync(OperateLogParameter parameter, IPropertyMapping propertyMapping = null)
        {
			var query = dbSet.AsQueryable();

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

            return new PaginatedList<OperateLog>(parameter.PageIndex, parameter.PageSize, count, data);
        }
    }
}