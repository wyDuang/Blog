using AutoMapper;
using WYBlog.Dtos;
using WYBlog.Entities;

namespace WYBlog
{
    public class BlogApplicationAutoMapperProfile : Profile
    {
        public BlogApplicationAutoMapperProfile()
        {
            CreateMap<Article, ArticleDto>().ReverseMap();
            CreateMap<ArticleDto, Article>().ReverseMap();
        }
    }
}