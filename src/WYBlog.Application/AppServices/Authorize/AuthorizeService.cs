using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using WYBlog.Configurations;
using WYBlog.Dtos;
using WYBlog.IAppServices;

namespace WYBlog.AppServices
{
    public class AuthorizeService : ApplicationService, IAuthorizeService
    {
        private const string Authorize_Prefix = CachePrefixConsts.Authorize;
        private const string KEY_GetLoginAddress = Authorize_Prefix + ":GetLoginAddress";
        private const string KEY_GetAccessToken = Authorize_Prefix + ":GetAccessToken-{0}";
        private const string KEY_GenerateToken = Authorize_Prefix + ":GenerateToken-{0}";

        public IDistributedCache Cache { get; set; }

        private readonly IHttpClientFactory _httpClient;

        public AuthorizeService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// 根据账户密码得到 Token
        /// </summary>
        /// <param name="accountInputDto"></param>
        /// <returns></returns>
        public async Task<string> GenerateTokenAsync(AccountInputDto accountInputDto)
        {
            if (accountInputDto.UserName == "wuyang" && accountInputDto.Password == "123456")
            {
                var authTime = DateTime.Now;//Nbf 生效时间，在此之前不可用
                var expiresAt = authTime.Add(TimeSpan.FromMinutes(AppSettings.JwtAuth.Expires));//Exp 过期时间，在此之后不可用

                var nbf = new DateTimeOffset(authTime).ToUnixTimeSeconds();
                var exp = new DateTimeOffset(expiresAt).ToUnixTimeSeconds();

                var claims = new[] {
                    new Claim(ClaimTypes.Name, accountInputDto.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),//唯一编号
                    new Claim(JwtRegisteredClaimNames.Nbf, $"{nbf}"),//生效时间
                    new Claim(JwtRegisteredClaimNames.Exp, $"{exp}")//过期时间
                };

                var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AppSettings.JwtAuth.SecurityKey));

                var securityToken = new JwtSecurityToken(
                    issuer: AppSettings.JwtAuth.Issuer,
                    audience: AppSettings.JwtAuth.Audience,
                    claims: claims,
                    notBefore: authTime,
                    expires: expiresAt,
                    signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256));

                var tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);

                var result = new
                {
                    access_token = tokenString,
                    token_type = "Bearer",
                    profile = new
                    {
                        name = accountInputDto.UserName,
                        auth_time = nbf,
                        expires_at = exp
                    }
                };

                return await Task.FromResult(JsonConvert.SerializeObject(result));
            }

            return await Task.FromResult("");
        }

        /// <summary>
        /// 获取登录地址(GitHub)
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public async Task<string> GetLoginAddressAsync()
        {
            return await Cache.GetOrCreateAsync(KEY_GetLoginAddress, async () =>
            {
                var address = string.Concat(new string[]
                {
                    AppSettings.GitHub.API_Authorize,
                    "?client_id=", AppSettings.GitHub.Client_ID,
                    "&scope=", AppSettings.GitHub.Scope,
                    "&state=", Guid.NewGuid().ToString("N"),
                    "&redirect_uri=", AppSettings.GitHub.Redirect_Uri
                });
                return await Task.FromResult(address);
            },
            CacheStrategyConsts.NEVER);
        }

        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<string> GetAccessTokenAsync(string code)
        {
            return await Cache.GetOrCreateAsync(string.Format(KEY_GetAccessToken, code), async () =>
            {
                var content = new StringContent($"code={code}&client_id={AppSettings.GitHub.Client_ID}&redirect_uri={AppSettings.GitHub.Redirect_Uri}&client_secret={AppSettings.GitHub.Client_Secret}");
                content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

                using var client = _httpClient.CreateClient();
                var httpResponse = await client.PostAsync(AppSettings.GitHub.API_AccessToken, content);

                var response = await httpResponse.Content.ReadAsStringAsync();
                if (response.StartsWith("access_token"))
                {
                    return response.Split("=")[1].Split("&").First();
                }
                return null;
            },
            CacheStrategyConsts.FIVE_MINUTES);
        }

        /// <summary>
        /// 登录成功，生成Token
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public async Task<string> GenerateTokenAsync(string access_token)
        {
            return await Cache.GetOrCreateAsync(string.Format(KEY_GenerateToken, access_token), async () =>
            {
                var url = $"{AppSettings.GitHub.API_User}?access_token={access_token}";

                using var client = _httpClient.CreateClient();
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.14 Safari/537.36 Edg/83.0.478.13");
                var httpResponse = await client.GetAsync(url);
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    var content = await httpResponse.Content.ReadAsStringAsync();
                    var user = JsonConvert.DeserializeObject<GitHubUserResponse>(content);
                    if (user?.Id == AppSettings.GitHub.UserId)
                    {
                        var authTime = DateTime.Now;//Nbf 生效时间，在此之前不可用
                        var expiresAt = authTime.Add(TimeSpan.FromMinutes(AppSettings.JwtAuth.Expires));//Exp 过期时间，在此之后不可用

                        var nbf = new DateTimeOffset(authTime).ToUnixTimeSeconds();
                        var exp = new DateTimeOffset(expiresAt).ToUnixTimeSeconds();

                        var claims = new[] {
                            new Claim(ClaimTypes.Name, user.Name),
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),//唯一编号
                            new Claim(JwtRegisteredClaimNames.Nbf, $"{nbf}"),//生效时间
                            new Claim(JwtRegisteredClaimNames.Exp, $"{exp}")//过期时间
                        };

                        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AppSettings.JwtAuth.SecurityKey));

                        var securityToken = new JwtSecurityToken(
                            issuer: AppSettings.JwtAuth.Issuer,
                            audience: AppSettings.JwtAuth.Audience,
                            claims: claims,
                            notBefore: authTime,
                            expires: expiresAt,
                            signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256));

                        var tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);

                        var result = new
                        {
                            access_token = tokenString,
                            token_type = "Bearer",
                            profile = new
                            {
                                name = user.Name,
                                auth_time = nbf,
                                expires_at = exp
                            }
                        };

                        return await Task.FromResult(JsonConvert.SerializeObject(result));
                    }
                }
                return await Task.FromResult("");
            },
            CacheStrategyConsts.FIVE_MINUTES);
        }

        /// <summary>
        /// 验证Token是否合法
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<bool> VerifyToken(string token)
        {
            var claims = new JwtSecurityToken(token).Claims;

            var name = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            var email = claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            if (name == AppSettings.GitHub.ApplicationName && !email.IsNullOrWhiteSpace())
            {
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
    }
}