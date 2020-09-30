using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using WYBlog.Dtos;
using WYBlog.Entities;
using WYBlog.IAppServices;
using WYBlog.IRepository;

namespace WYBlog.AppServices
{
    public class ArticleService : ApplicationService, IArticleService
    {
        private readonly IArticleRepository _repository;

        public ArticleService(IArticleRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 分页查询文章列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<ArticleDto>> GetPagedListAsync(QueryArticleDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Article.CreateTime);
            }

            var tagList = await _repository.GetPagedListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );

            var totalCount = await AsyncExecuter.CountAsync(
                _repository.WhereIf(!input.Filter.IsNullOrWhiteSpace(), x => x.Title.Contains(input.Filter)
                )
            );

            return new PagedResultDto<ArticleDto>(
                totalCount,
                ObjectMapper.Map<List<Article>, List<ArticleDto>>(tagList)
            );
        }

        /// <summary>
        /// 通过Id获取文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ArticleDto> GetAsync(int id)
        {
            var articleEntity = await _repository.GetAsync(x => x.Id == id);
            return ObjectMapper.Map<Article, ArticleDto>(articleEntity);
        }

        /// <summary>
        /// 通过Key获取文章
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<ArticleDto> GetByKeyAsync(string key)
        {
            var articleEntity = await _repository.GetAsync(x => x.ArticleKey == key);
            return ObjectMapper.Map<Article, ArticleDto>(articleEntity);
        }

        /// <summary>
        /// 新增文章
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ArticleDto> CreateAsync(CreateOrEditArticleDto input)
        {
            var articleEntity = ObjectMapper.Map<CreateOrEditArticleDto, Article>(input);
            var resultEntity = await _repository.InsertAsync(articleEntity, true);

            return ObjectMapper.Map<Article, ArticleDto>(resultEntity);
        }

        /// <summary>
        /// 编辑文章
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateAsync(int id, CreateOrEditArticleDto input)
        {
            var articleEntity = await _repository.GetAsync(x => x.Id == id);
            articleEntity = ObjectMapper.Map<CreateOrEditArticleDto, Article>(input);

            await _repository.UpdateAsync(articleEntity);
        }

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(x => x.Id == id);
        }
    }
}