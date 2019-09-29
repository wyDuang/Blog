using System;
using System.Collections.Generic;
using System.Text;
using WyBlog.IRepository;
using WyBlog.IServices;

namespace WyBlog.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;

        public ArticleService(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public void MyProperty()
        {

        }
    }
}
