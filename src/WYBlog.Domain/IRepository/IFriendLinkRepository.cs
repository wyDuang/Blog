using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using WYBlog.Entities;

namespace WYBlog.IRepository
{
    public interface IFriendLinkRepository : IRepository<FriendLink, int>
    {
        Task<List<FriendLink>> GetPagedListAsync(int skipCount, int maxResultCount, string sorting, string filter = null);

        Task<FriendLink> FindByTitleAsync(string title);
    }
}