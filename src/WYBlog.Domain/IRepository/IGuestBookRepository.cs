using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using WYBlog.Entities;

namespace WYBlog.IRepository
{
    public interface IGuestBookRepository : IRepository<GuestBook, int>
    {
        Task<List<GuestBook>> GetPagedListAsync(int skipCount, int maxResultCount, string sorting, int parentId, string filter = null);
    }
}