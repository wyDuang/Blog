using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using WYBlog.Dtos;

namespace WYBlog
{
    public interface IArticleService : IApplicationService
    {
        Task<List<ArticleDto>> GetListAsync();
    }
}