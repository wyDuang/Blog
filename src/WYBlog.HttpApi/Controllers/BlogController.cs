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
        private readonly IFriendLinkService _friendLinkService;
        private readonly IGuestBookService _guestBookService;
        private readonly ICategoryService _categoryService;
        private readonly IArticleService _articleService;
        private readonly ITagService _tagService;
        public BlogController(
            IFriendLinkService friendLinkService,
            IGuestBookService guestBookService,
            ICategoryService categoryService,
            IArticleService articleService,
            ITagService tagService)
        {
            _friendLinkService = friendLinkService;
            _guestBookService = guestBookService;
            _categoryService = categoryService;
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
        public async Task<IActionResult> QueryArticlesAsync(QueryArticleDto input)
        {
            var result = await _articleService.GetPagedListAsync(input);
            return Ok(result);
        }

        #endregion

        #region Tag

        /// <summary>
        /// 查询标签列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("tags")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> QueryAllArticlesAsync()
        {
            var result = await _tagService.GetAllListAsync();
            return Ok(result);
        }

        [HttpPost("tag")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
        public async Task<IActionResult> AddTag([FromBody] CreateOrEditTagDto input)
        {
            if (null == input) return BadRequest();

            var result = await _tagService.CreateAsync(input);
            return Ok(result);
        }

        [HttpPut("tag/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
        public async Task<IActionResult> EditTag(int id, [FromBody] CreateOrEditTagDto input)
        {
            await _tagService.UpdateAsync(id, input);
            return Ok();
        }

        #endregion
    }
}