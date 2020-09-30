using AutoMapper;
using Volo.Abp.AutoMapper;
using WYBlog.Dtos;
using WYBlog.Entities;

namespace WYBlog
{
    public class BlogApplicationAutoMapperProfile : Profile
    {
        public BlogApplicationAutoMapperProfile()
        {
            CreateMap<CreateOrEditArticleDto, Article>();
            CreateMap<Article, ArticleDto>()
                .ForMember(dest => (int)dest.ArticleType, opt => opt.MapFrom(src => src.ArticleType));

            CreateMap<Tag, TagDto>();
            CreateMap<CreateOrEditTagDto, Tag>();//.Ignore(x => x.Id);


        }
    }
}