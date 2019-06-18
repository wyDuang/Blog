using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WyBlog.Authentication;
using WyBlog.Core.Models;
using WyBlog.Dtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WyBlog.Api.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        protected JwtSettings _jwtSettings;
        protected AppSettings _appSettings;

        public AuthController(
            IOptionsSnapshot<JwtSettings> jwtOptions,
            IOptionsSnapshot<AppSettings> appSettings)
        {
            _jwtSettings = jwtOptions.Value;
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
                var claims = new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, account.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Exp, $"{new DateTimeOffset(DateTime.Now.AddMinutes(_jwtSettings.ExpireMinutes)).ToUnixTimeSeconds()}"),
                    new Claim(JwtRegisteredClaimNames.Nbf, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}")
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecurityKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: _jwtSettings.Issuer,
                    audience: _jwtSettings.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(_jwtSettings.ExpireMinutes),
                    signingCredentials: creds);

                var response = new {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    token_type = "Bearer"
                };
                return Ok(response);
            }
            return Unauthorized();
        }
    }
}
