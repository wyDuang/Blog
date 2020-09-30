using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using WYBlog.Dtos;
using WYBlog.IAppServices;

namespace WYBlog.Controllers
{
    [ApiExplorerSettings(GroupName = ApiGroupingConsts.GroupName_v1)]
    public class BlogController : BaseController
    {
        private readonly IArticleService _articleService;
        private readonly ITagService _tagService;

        public BlogController(
            IArticleService articleService,
            ITagService tagService)
        {
            _articleService = articleService;
            _tagService = tagService;
        }

        #region article

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

        #endregion

        #region Tag

        [AllowAnonymous]
        [HttpPost("tag", Name = "GetTag")]
        public async Task<IActionResult> AddTag([FromBody] CreateOrEditTagDto input)
        {
            if (null == input) return BadRequest();

            var result = await _tagService.CreateAsync(input);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPut("tag/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> EditTag(int id, [FromBody] CreateOrEditTagDto input)
        {
            await _tagService.UpdateAsync(id, input);
            return Ok();
        }

        #endregion
    }
}