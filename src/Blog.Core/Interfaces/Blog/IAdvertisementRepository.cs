using System.Threading.Tasks;
using Blog.Core.BaseModels;
using Blog.Core.Entities;

namespace Blog.Core.Interfaces
{
    /// <summary>
    /// 广告表（仓储层）
    /// </summary>
    public interface IAdvertisementRepository : IRepositoryBase<Advertisement>
    {
		Task<PaginatedList<Advertisement>> GetPageListAsync(AdvertisementParameter parameter, IPropertyMapping propertyMapping = null);
    }
}