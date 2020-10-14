using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using WYBlog.Dtos;

namespace WYBlog.IAppServices
{
    public interface IAuthorizeService : IApplicationService
    {
        /// <summary>
        /// 根据账户密码得到 Token
        /// </summary>
        /// <param name="accountInputDto"></param>
        /// <returns></returns>
        Task<string> GenerateTokenAsync(AccountInputDto accountInputDto);

        /// <summary>
        /// 获取登录地址(GitHub)
        /// </summary>
        /// <returns></returns>
        Task<string> GetLoginAddressAsync();

        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<string> GetAccessTokenAsync(string code);

        /// <summary>
        /// 登录成功，生成Token
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns></returns>
        Task<string> GenerateTokenAsync(string access_token);

        /// <summary>
        /// 验证Token是否合法
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<bool> VerifyToken(string token);
    }
}