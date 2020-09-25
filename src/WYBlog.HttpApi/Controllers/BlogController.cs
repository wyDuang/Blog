using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using WYBlog.IAppServices;

namespace WYBlog.Controllers
{
    [Authorize]
    [ApiExplorerSettings(GroupName = ApiGroupingConsts.GroupName_v1)]
    public class BlogController : BaseController
    {
        private readonly IArticleService _articleService;

        public BlogController(IArticleService articleService)
        {
            _articleService = articleService;
        }


        /// <summary>
        /// 查询文章列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("article")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> QueryArticlesAsync()
        {
            var result = await _articleService.GetListAsync();
            return Ok(result);
        }
    }
}