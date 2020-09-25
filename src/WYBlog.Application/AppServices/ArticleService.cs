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
    [RemoteService(IsEnabled = false)]
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
    }
}