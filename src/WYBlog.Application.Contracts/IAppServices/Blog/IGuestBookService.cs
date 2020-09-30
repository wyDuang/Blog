using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using WYBlog.Dtos;

namespace WYBlog.IAppServices
{
    public interface IGuestBookService : IApplicationService
    {
        /// <summary>
        /// 分页查询留言列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<GuestBookDto>> GetPagedListAsync(QueryGuestBookDto input);

        /// <summary>
        /// 通过Id获取留言
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<GuestBookDto> GetAsync(int id);

        /// <summary>
        /// 新增留言
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GuestBookDto> CreateAsync(CreateOrEditGuestBookDto input);

        /// <summary>
        /// 编辑留言
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateAsync(int id, CreateOrEditGuestBookDto input);

        /// <summary>
        /// 删除留言
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
