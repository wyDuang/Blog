using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using WYBlog.Dtos;

namespace WYBlog.IAppServices
{
    public interface ICategoryService : IApplicationService
    {
        /// <summary>
        /// 分页查询文章分类列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<CategoryDto>> GetPagedListAsync(QueryCategoryDto input);

        /// <summary>
        /// 通过Id获取文章分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CategoryDto> GetAsync(int id);

        /// <summary>
        /// 通过Key获取文章分类
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<CategoryDto> GetByKeyAsync(string key);

        /// <summary>
        /// 新增文章分类
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<CategoryDto> CreateAsync(CreateOrEditCategoryDto input);

        /// <summary>
        /// 编辑文章分类
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateAsync(int id, CreateOrEditCategoryDto input);

        /// <summary>
        /// 删除文章分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}