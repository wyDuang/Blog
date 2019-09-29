using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WyBlog.Dtos;

namespace WyBlog.IServices
{
    public interface IArticleService
    {
        void MyProperty();
        Task InsertAsync(ArticleAddDto dto);
    }
}
