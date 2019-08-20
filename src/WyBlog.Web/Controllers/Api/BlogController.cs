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

namespace WyBlog.Web.Controllers.Api
{
    [ApiController]
    public class BlogController : ControllerBase
    {
        private JwtSettings _jwtSettings;
        private AppSettings _appSettings;

        public BlogController(
            IOptions<JwtSettings> jwtSettings,
            IOptionsSnapshot<AppSettings> appSettings)
        {
            _jwtSettings = jwtSettings.Value;
            _appSettings = appSettings.Value;
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
