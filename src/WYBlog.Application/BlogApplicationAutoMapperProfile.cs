using AutoMapper;
using WYBlog.Dtos;
using WYBlog.Entities;

namespace WYBlog
{
    public class BlogApplicationAutoMapperProfile : Profile
    {
        public BlogApplicationAutoMapperProfile()
        {
            CreateMap<Article, ArticleDto>();

            CreateMap<Tag, TagDto>();

            //CreateMap<Tag, CreateOrEditTagDto>();
            CreateMap<CreateOrEditTagDto, Tag>().ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}