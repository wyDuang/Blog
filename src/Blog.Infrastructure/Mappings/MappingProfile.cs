using AutoMapper;
using Blog.Core.Entities;
using Blog.Infrastructure.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Article, ArticleResource>()
                .ForMember(dest => dest.IsTop == true ? 1 : 0, opt => opt.MapFrom(src => src.IsTop));

            CreateMap<ArticleResource, Article>();
            CreateMap<ArticleAddResource, Article>();
            CreateMap<ArticleUpdateResource, Article>();
            
                
        }
    }
}
