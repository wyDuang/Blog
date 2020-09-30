using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;
using WYBlog.Dtos;
using WYBlog.Entities;
using WYBlog.IAppServices;

namespace WYBlog.AppServices
{
    public class ArticleService : ApplicationService, IArticleService
    {
        private readonly IRepository<Article> _repository;
        private readonly IUnitOfWorkManager _unitOfWork;

        public ArticleService(IRepository<Article> repository, IUnitOfWorkManager unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ArticleDto>> GetListAsync()
        {
            var entities = await _repository.GetListAsync();

            return ObjectMapper.Map<List<Article>, List<ArticleDto>>(entities);
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