using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WyBlog.Core.Models;
using WyBlog.Dtos;
using WyBlog.Entities;
using WyBlog.IServices;

namespace WyBlog.Web.Controllers.Api
{
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IArticleService _articleService;
        private JwtSettings _jwtSettings;
        private AppSettings _appSettings;

        public BlogController(
            IArticleService articleService,
            IOptions<JwtSettings> jwtSettings,
            IOptionsSnapshot<AppSettings> appSettings)
        {
            _jwtSettings = jwtSettings.Value;
            _appSettings = appSettings.Value;
            _articleService = articleService;
        }

        /// <summary>
        /// 新增文章
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("post")]
        public async Task<IActionResult> AddArticle([FromBody] ArticleAddDto dto)
        {
            await _articleService.InsertAsync(dto);
            //var response = new Response<string>();

            //var result = await _blogService.InsertPost(dto);
            //if (!result.Success)
            //    response.SetMessage(ResponseStatusCode.Error, result.GetErrorMessage());
            //else
            //    response.Result = result.Result;
            return Ok();
        }


    }
}
