using AutoMapper;
using Blog.Core.Entities;
using Blog.Infrastructure.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.AutoMapper
{
    public class MappingProfile : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public MappingProfile()
        {
            CreateMap<Article, ArticleResource>()
                .ForMember(dest => dest.IsTop, opt => opt.MapFrom(src => src.IsTop));
            CreateMap<ArticleResource, Article>();

            //CreateMap<Article, ArticleAddResource>();

            CreateMap<Article, ArticleAddOrUpdateResource>()
                .ForMember(dest => dest.IsTop, opt => opt.MapFrom(src => src.IsTop));
            CreateMap<ArticleAddResource, Article>();
            CreateMap<ArticleUpdateResource, Article>();
        }
    }
}
