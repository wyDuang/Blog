using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WyBlog.Core.Models;
using WyBlog.Entities;

namespace WyBlog.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private JwtSettings _jwtSettings;
        private AppSettings _appSettings;

        public AccountController(
            IOptions<JwtSettings> jwtSettings,
            IOptionsSnapshot<AppSettings> appSettings)
        {
            _jwtSettings = jwtSettings.Value;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// 获取Token
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [Route("api/token")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult GetToken(AccountEntity account)
        {
            if (account == null) return Unauthorized();

            if (account.UserName == _appSettings.UserName && account.Password == _appSettings.Password)
            {
                var authTime = DateTime.Now;//Nbf 生效时间，在此之前不可用
                var expiresAt = authTime.AddMinutes(_jwtSettings.ExpireMinutes);//Exp 过期时间，在此之后不可用
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecurityKey));

                var claims = new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, account.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Nbf, $"{new DateTimeOffset(authTime).ToUnixTimeSeconds()}"),
                    new Claim(JwtRegisteredClaimNames.Exp, $"{new DateTimeOffset(expiresAt).ToUnixTimeSeconds()}")
                };
                var token = new JwtSecurityToken(
                    issuer: _jwtSettings.Issuer,
                    audience: _jwtSettings.Audience,
                    claims: claims,
                    expires: expiresAt,
                    signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                var result = new
                {
                    access_token = tokenString,
                    token_type = "Bearer",
                    profile = new
                    {
                        name = account.UserName,
                        auth_time = new DateTimeOffset(authTime).ToUnixTimeSeconds(),
                        expires_at = new DateTimeOffset(expiresAt).ToUnixTimeSeconds()
                    }
                };
                return Ok(result);
            }
            return Unauthorized("The moonlight in front of the bed, two pairs of shoes on the ground. Transfer the dog to men and women, including you.");
        }




        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("logout")]
        public async Task<bool> LogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return true;
        }
    }
}
