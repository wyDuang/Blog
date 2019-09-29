using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WyBlog.Dtos;
using WyBlog.Entities;
using WyBlog.IRepository;
using WyBlog.IServices;

namespace WyBlog.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ArticleService(IArticleRepository articleRepository, IUnitOfWork unitOfWork)
        {
            _articleRepository = articleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task InsertAsync(ArticleAddDto dto)
        {
            Article article = new Article();

            _articleRepository.Insert(article);
            await _unitOfWork.SaveAsync();
        }

        public void MyProperty()
        {
            throw new NotImplementedException();
        }
    }
}
