using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using WYBlog.Dtos;
using WYBlog.IAppServices;
using WYBlog.Permissions;

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

        #region Article

        /// <summary>
        /// 根据Id获取文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("articles/{id:min(1)}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Authorize(BlogPermissions.Articles.Default)]
        public async Task<IActionResult> GetArticleAsync(int id)
        {
            if (0 >= id) return BadRequest();

            var result = await _articleService.GetAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// 根据key获取文章
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet("articles/{key}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Authorize(BlogPermissions.Articles.Default)]
        public async Task<IActionResult> GetArticleAsync(string key)
        {
            if (key.IsNullOrWhiteSpace()) return BadRequest();

            var result = await _articleService.GetByKeyAsync(key);
            return Ok(result);
        }


        /// <summary>
        /// 查询文章列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("articles")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Authorize(BlogPermissions.Articles.Default)]
        public async Task<IActionResult> QueryArticlesAsync(QueryArticleDto input)
        {
            if (null == input) return BadRequest();

            var result = await _articleService.GetPagedListAsync(input);
            return Ok(result);
        }

        /// <summary>
        /// 新增文章
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("articles")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
        [Authorize(BlogPermissions.Articles.Create)]
        public async Task<IActionResult> AddArticle([FromBody] CreateOrEditArticleDto input)
        {
            if (null == input) return BadRequest();

            var result = await _articleService.CreateAsync(input);
            return Ok(result);
        }

        /// <summary>
        /// 编辑文章
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("articles/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
        [Authorize(BlogPermissions.Articles.Edit)]
        public async Task<IActionResult> EditArticle(int id, [FromBody] CreateOrEditArticleDto input)
        {
            await _articleService.UpdateAsync(id, input);
            return Ok();
        }

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("articles/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [Authorize(BlogPermissions.Articles.Delete)]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            await _articleService.DeleteAsync(id);
            return Ok();
        }

        #endregion

        #region Category

        /// <summary>
        /// 查询所有分类列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("categories")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> QueryAllCategoriesAsync()
        {
            var result = await _categoryService.GetAllListAsync();
            return Ok(result);
        }

        /// <summary>
        /// 新增分类
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("categories")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
        public async Task<IActionResult> AddCategory([FromBody] CreateOrEditCategoryDto input)
        {
            if (null == input) return BadRequest();

            var result = await _categoryService.CreateAsync(input);
            return Ok(result);
        }

        /// <summary>
        /// 编辑分类
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("categories/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
        public async Task<IActionResult> EditCategory(int id, [FromBody] CreateOrEditCategoryDto input)
        {
            await _categoryService.UpdateAsync(id, input);
            return Ok();
        }

        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("categories/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteAsync(id);
            return Ok();
        }

        #endregion

        #region Tag

        /// <summary>
        /// 查询所有标签列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("tags")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> QueryAllTagsAsync()
        {
            var result = await _tagService.GetAllListAsync();
            return Ok(result);
        }

        /// <summary>
        /// 新增标签
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("tags")]
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

        /// <summary>
        /// 编辑标签
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("tags/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
        public async Task<IActionResult> EditTag(int id, [FromBody] CreateOrEditTagDto input)
        {
            await _tagService.UpdateAsync(id, input);
            return Ok();
        }

        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("tags/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> DeleteTag(int id)
        {
            await _tagService.DeleteAsync(id);
            return Ok();
        }

        #endregion

        #region FriendLink

        /// <summary>
        /// 查询友情链接列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("friendlinks")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> QueryFriendLinksAsync(QueryFriendLinkDto input)
        {
            var result = await _friendLinkService.GetPagedListAsync(input);
            return Ok(result);
        }

        /// <summary>
        /// 添加友情链接
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("friendlinks")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
        public async Task<IActionResult> AddFriendLink([FromBody] CreateOrEditFriendLinkDto input)
        {
            if (null == input) return BadRequest();

            var result = await _friendLinkService.CreateAsync(input);
            return Ok(result);
        }

        /// <summary>
        /// 编辑友情链接
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("friendlinks/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
        public async Task<IActionResult> EditFriendLink(int id, [FromBody] CreateOrEditFriendLinkDto input)
        {
            await _friendLinkService.UpdateAsync(id, input);
            return Ok();
        }

        /// <summary>
        /// 删除友情链接
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("friendlinks/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> DeleteFriendLink(int id)
        {
            await _friendLinkService.DeleteAsync(id);
            return Ok();
        }

        #endregion

        #region GuestBook

        /// <summary>
        /// 查询留言列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("guestbooks")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> QueryGuestBooksAsync(QueryGuestBookDto input)
        {
            var result = await _guestBookService.GetPagedListAsync(input);
            return Ok(result);
        }

        /// <summary>
        /// 添加留言
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("guestbooks")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
        public async Task<IActionResult> AddFriendLink([FromBody] CreateOrEditGuestBookDto input)
        {
            if (null == input) return BadRequest();

            var result = await _guestBookService.CreateAsync(input);
            return Ok(result);
        }

        /// <summary>
        /// 编辑留言
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("guestbooks/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
        public async Task<IActionResult> EditGuestBook(int id, [FromBody] CreateOrEditGuestBookDto input)
        {
            await _guestBookService.UpdateAsync(id, input);
            return Ok();
        }

        /// <summary>
        /// 删除留言
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("guestbooks/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> DeleteGuestBook(int id)
        {
            await _guestBookService.DeleteAsync(id);
            return Ok();
        }

        #endregion
    }
}