using AutoMapper;
using Blog.Core.Entities;
using Blog.Core.Interfaces;
using Blog.Core.SettingModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Site.Controllers.Api
{
    [AllowAnonymous]
    public class AccountController : ApiBaseController
    {
        private readonly IHttpClientFactory _client;
        private readonly JwtSettings _jwtSettings;
        private readonly AppSettings _appSettings;
        public AccountController(
            IHttpClientFactory client,
            IOptions<JwtSettings> jwtSettings,
            IOptionsSnapshot<AppSettings> appSettings,
            IUnitOfWork unitOfWork,
            ILogger<AccountController> logger,
            IMapper mapper,
            IUrlHelper urlHelper,
            ITypeHelperService typeHelperService,
            IPropertyMappingContainer propertyMappingContainer)
            : base(unitOfWork, logger, mapper, urlHelper, typeHelperService, propertyMappingContainer)
        {
            _client = client;
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
                var expiresAt = authTime.Add(TimeSpan.FromMinutes(_jwtSettings.TokenExpiresMinutes));//Exp 过期时间，在此之后不可用
                
                var claims = new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, account.UserName),
                    new Claim(JwtRegisteredClaimNames.Nbf, $"{new DateTimeOffset(authTime).ToUnixTimeSeconds()}"),
                    new Claim(JwtRegisteredClaimNames.Exp, $"{new DateTimeOffset(expiresAt).ToUnixTimeSeconds()}")
                };

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.IssuerSigningKey));
                var token = new JwtSecurityToken(
                    issuer: _jwtSettings.Issuer,
                    audience: _jwtSettings.Audience,
                    claims: claims,
                    notBefore: authTime,
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
    }
}
