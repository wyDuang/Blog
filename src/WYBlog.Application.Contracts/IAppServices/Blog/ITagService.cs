using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using WYBlog.Dtos;

namespace WYBlog.IAppServices
{
    public interface ITagService : IApplicationService
    {
        /// <summary>
        /// 分页查询标签列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<TagDto>> GetPagedListAsync(QueryTagDto input);

        /// <summary>
        /// 通过Id获取标签
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TagDto> GetAsync(int id);

        /// <summary>
        /// 通过Key获取标签
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<TagDto> GetByKeyAsync(string key);

        /// <summary>
        /// 新增标签
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<TagDto> CreateAsync(CreateOrEditTagDto input);

        /// <summary>
        /// 编辑标签
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateAsync(int id, CreateOrEditTagDto input);

        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}