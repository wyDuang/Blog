using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using WYBlog.Dtos;

namespace WYBlog.IAppServices
{
    public interface IFriendLinkService : IApplicationService
    {
        /// <summary>
        /// 分页查询友情链接列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<FriendLinkDto>> GetPagedListAsync(QueryFriendLinkDto input);

        /// <summary>
        /// 通过Id获取友情链接
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<FriendLinkDto> GetAsync(int id);

        /// <summary>
        /// 新增友情链接
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<FriendLinkDto> CreateAsync(CreateOrEditFriendLinkDto input);

        /// <summary>
        /// 编辑友情链接
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateAsync(int id, CreateOrEditFriendLinkDto input);

        /// <summary>
        /// 删除友情链接
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}