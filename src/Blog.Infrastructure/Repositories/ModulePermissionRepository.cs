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
	/// 菜单与按钮关系表（仓储层实现）
	/// </summary>
    public class ModulePermissionRepository : EFRepository<ModulePermission>, IModulePermissionRepository
    {
        public ModulePermissionRepository(MyContext myContext) : base(myContext)
        {
        }

        public async Task<PaginatedList<ModulePermission>> GetPageListAsync(ModulePermissionParameter parameter, IPropertyMapping propertyMapping = null)
        {
			var query = Context.Set<ModulePermission>().AsQueryable();

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

            return new PaginatedList<ModulePermission>(parameter.PageIndex, parameter.PageSize, count, data);
        }

    }
}