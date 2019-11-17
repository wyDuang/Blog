using System.Threading.Tasks;
using Blog.Core.BaseModels;
using Blog.Core.Entities;

namespace Blog.Core.Interfaces
{
    /// <summary>
    /// 留言表（仓储层）
    /// </summary>
    public interface IGuestBookRepository : IRepositoryBase<GuestBook>
    {
		Task<PaginatedList<GuestBook>> GetPageListAsync(GuestBookParameter Parameter, IPropertyMapping propertyMapping = null);
    }
}