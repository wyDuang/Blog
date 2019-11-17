using AutoMapper;
using Blog.Core.Entities;
using Blog.Core.Interfaces;
using Blog.Core.SettingModels;
using Blog.Infrastructure.Extensions;
using Blog.Infrastructure.Resources;
using Blog.Infrastructure.ResultModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Blog.Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpClientFactory _httpClient;
        private readonly JwtSettings _jwtSettings;
        private readonly GitHubSettings _gitHubSettings;
        //public AuthController(
        //    IHttpClientFactory httpClient,
        //    IOptions<JwtSettings> jwtSettings,
        //    IOptions<GitHubSettings> gitHubSettings,
        //    IUnitOfWork unitOfWork,
        //    ILogger<AuthController> logger,
        //    IMapper mapper,
        //    IUrlHelper urlHelper,
        //    ITypeHelperService typeHelperService,
        //    IPropertyMappingContainer propertyMappingContainer)
        //    : base(unitOfWork, logger, mapper, urlHelper, typeHelperService, propertyMappingContainer)
        //{
        //    _httpClient = httpClient;
        //    _jwtSettings = jwtSettings.Value;
        //    _gitHubSettings = gitHubSettings.Value;
        //}

        public AuthController(
            IUserRepository userRepository,
            IHttpClientFactory httpClient,
            IOptions<JwtSettings> jwtSettings,
            IOptions<GitHubSettings> gitHubSettings)
        {
            _userRepository = userRepository;
            _httpClient = httpClient;
            _jwtSettings = jwtSettings.Value;
            _gitHubSettings = gitHubSettings.Value;
        }

        /// <summary>
        /// Post 获取Token
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public IActionResult PostToken(LoginResource loginResource)
        {
            if (loginResource == null) return BadRequest();

            if (!ModelState.IsValid)
            {
                return new MyUnprocessableEntityObjectResult(ModelState);
            }
            var user = _userRepository.GetUserByNameAndPwd(loginResource.Username, loginResource.Password);
            if(user != null)
            {
                var authTime = DateTime.Now;//Nbf 生效时间，在此之前不可用
                var expiresAt = authTime.Add(TimeSpan.FromMinutes(_jwtSettings.AccessTokenExpiresMinutes));//Exp 过期时间，在此之后不可用

                var claims = new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, loginResource.Username),
                    new Claim(JwtRegisteredClaimNames.Nbf, $"{new DateTimeOffset(authTime).ToUnixTimeSeconds()}"),
                    new Claim(JwtRegisteredClaimNames.Exp, $"{new DateTimeOffset(expiresAt).ToUnixTimeSeconds()}")
                };

                var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.IssuerSigningKey));
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
                        name = loginResource.Username,
                        auth_time = new DateTimeOffset(authTime).ToUnixTimeSeconds(),
                        expires_at = new DateTimeOffset(expiresAt).ToUnixTimeSeconds()
                    }
                };
                return Ok(result);
            }
            return Unauthorized("The moonlight in front of the bed, two pairs of shoes on the ground. Transfer the dog to men and women, including you.");
        }

        /// <summary>
        /// 获取Github 登录地址 最终得到code
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("login")]
        public IActionResult GetGithubLoginUrlAsync()
        {
            //https://developer.github.com/apps/building-oauth-apps/authorizing-oauth-apps/

            //scope：该参数可选，需要调用Github哪些信息，可以填写多个，以逗号分割，比如：scope=user,public_repo。
            //       如果不填写，那么你的应用程序将只能读取Github公开的信息，比如公开的用户信息，公开的库(repository)信息以及gists信息

            //State：不可猜测的随机字符串。它用于防止跨站点请求伪造攻击。

            var response = $"{_gitHubSettings.API_Authorize}?client_id={_gitHubSettings.Client_Id}&scope=user,public_repo&state={Guid.NewGuid().ToString("N")}&redirect_uri={_gitHubSettings.Redirect_Uri}";

            return Ok(response);
        }

        /// <summary>
        /// 获取Github access_token
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("access_token")]
        public async Task<IActionResult> GetAccessTokenAsync(string code)
        {
            if (code.IsNullOrWhiteSpace()) return BadRequest();//发送的请求是错误的

            var client = _httpClient.CreateClient("github");
            var content = new StringContent($"code={code}&client_id={_gitHubSettings.Client_Id}&redirect_uri={_gitHubSettings.Redirect_Uri}&client_secret={_gitHubSettings.Client_Secret}");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            var httpResponse = await client.PostAsync(_gitHubSettings.API_AccessToken,content);
            if (httpResponse.StatusCode != HttpStatusCode.OK)
                return Unauthorized();//参数Code无效

            var result = await httpResponse.Content.ReadAsStringAsync();
            if (result.StartsWith("access_token"))
                return Ok(result.Split("=")[1].Split("&").First());
            return NotFound("access_token不存在！");//请求的资源不存在
        }

        /// <summary>
        /// 通过 github的 access_token 生成 Jwt Token
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("token")]
        public async Task<IActionResult> GenerateTokenAsync(string access_token)
        {
            if (access_token.IsNullOrWhiteSpace()) return BadRequest();

            var client = _httpClient.CreateClient("github");
            var url = $"{_gitHubSettings.API_User}?access_token={access_token}";

            var httpResponse = await client.GetAsync(url);
            if (httpResponse.StatusCode != HttpStatusCode.OK)
                return Unauthorized();//参数access_token无效

            var content = await httpResponse.Content.ReadAsStringAsync();
            var userResource = JsonSerializer.Deserialize<GitHubUserResource>(content);
            if (null == userResource)
                return NotFound();//github user资源不存在

            //TODO 这里要判断数据库存在


            return Ok();
        }
    }
}
