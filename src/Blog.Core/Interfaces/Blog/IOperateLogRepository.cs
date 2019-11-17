using System.Threading.Tasks;
using Blog.Core.BaseModels;
using Blog.Core.Entities;

namespace Blog.Core.Interfaces
{
	/// <summary>
	/// 操作日志表（仓储层）
	/// </summary>
    public interface IOperateLogRepository : IRepositoryBase<OperateLog>
    {
		Task<PaginatedList<OperateLog>> GetPageListAsync(OperateLogParameter parameter, IPropertyMapping propertyMapping = null);
    }
}