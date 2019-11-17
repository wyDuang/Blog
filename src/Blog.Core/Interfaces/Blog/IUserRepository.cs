using Blog.Core.BaseModels;
using Blog.Core.Entities;
using System.Threading.Tasks;

namespace Blog.Core.Interfaces
{
    /// <summary>
    /// 用户表（仓储层）
    /// </summary>
    public interface IUserRepository : IRepositoryBase<User>
    {
		Task<PaginatedList<User>> GetPageListAsync(UserParameter parameter, IPropertyMapping propertyMapping = null);

        User GetUserByNameAndPwd(string userName, string password);
    }
}