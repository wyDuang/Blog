using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NUglify.Helpers;
using System.Threading.Tasks;
using WYBlog.Dtos;
using WYBlog.IAppServices;

namespace WYBlog.Controllers
{
    /// <summary>
    /// Auth Api
    /// </summary>
    /// <returns>JSON Web Token</returns>
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = false)]//默认是false，当为true表示不显示到swaggerUI
    public class AuthController : BaseController
    {
        private readonly IAuthorizeService _authorizeService;

        public AuthController(IAuthorizeService authorizeService)
        {
            _authorizeService = authorizeService;
        }

        /// <summary>
        /// 根据账号密码生成Token
        /// </summary>
        /// <param name="accountInputDto"></param>
        /// <returns></returns>
        [HttpGet("token")]
        public async Task<IActionResult> GenerateTokenAsync(AccountInputDto accountInputDto)
        {
            if (null == accountInputDto) return BadRequest();

            var token = await _authorizeService.GenerateTokenAsync(accountInputDto);
            if (!token.IsNullOrWhiteSpace())
            {
                return Ok(token);
            }
            return NotFound("token不存在！");
        }

        /// <summary>
        /// 获取登录地址(GitHub)
        /// </summary>
        /// <returns></returns>
        [HttpGet("github/url")]
        public async Task<IActionResult> GetLoginAddressAsync()
        {
            return Ok(await _authorizeService.GetLoginAddressAsync());
        }

        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("github/access_token")]
        public async Task<IActionResult> GetAccessTokenAsync(string code)
        {
            if (code.IsNullOrWhiteSpace()) return BadRequest();

            var accessToken = await _authorizeService.GetAccessTokenAsync(code);
            if (!accessToken.IsNullOrWhiteSpace())
            {
                return Ok(accessToken);
            }
            return NotFound("access_token不存在！");
        }

        /// <summary>
        /// 登录成功，生成Token
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns></returns>
        [HttpGet("github/token")]
        public async Task<IActionResult> GenerateTokenAsync(string access_token)
        {
            if (access_token.IsNullOrWhiteSpace()) return BadRequest();

            var token = await _authorizeService.GenerateTokenAsync(access_token);
            if (!token.IsNullOrWhiteSpace())
            {
                return Ok(token);
            }
            return NotFound("token不存在！");
        }

        /// <summary>
        /// 验证Token是否合法
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("verify_token")]
        public async Task<IActionResult> VerifyToken(string token)
        {
            if (token.IsNullOrWhiteSpace()) return BadRequest();

            var isLegal = await _authorizeService.VerifyToken(token);
            if (isLegal)
            {
                return Ok(isLegal);
            }
            return NotFound("token无效！");
        }
    }
}