using System.Threading.Tasks;
using Blog.Core.BaseModels;
using Blog.Core.Entities;

namespace Blog.Core.Interfaces
{
	/// <summary>
	/// 接口API地址信息表（仓储层）
	/// </summary>
    public interface IModuleRepository : IRepositoryBase<Module>
    {
		Task<PaginatedList<Module>> GetPageListAsync(ModuleParameter parameter, IPropertyMapping propertyMapping = null);
    }
}