﻿using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using WYBlog.Dtos;

namespace WYBlog.IAppServices
{
    public interface IArticleService : IApplicationService
    {
        /// <summary>
        /// 分页查询文章列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<ArticleDto>> GetPagedListAsync(QueryArticleDto input);

        /// <summary>
        /// 通过Id获取文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ArticleDto> GetAsync(int id);

        /// <summary>
        /// 通过Key获取文章
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<ArticleDto> GetByKeyAsync(string key);

        /// <summary>
        /// 新增文章
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ArticleDto> CreateAsync(CreateOrEditArticleDto input);

        /// <summary>
        /// 编辑文章
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateAsync(int id, CreateOrEditArticleDto input);

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}