using Blog.Core.BaseModels;
using Blog.Core.Entities;
using Blog.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Resources.PropertyMappings
{
    public class ArticlePropertyMapping : PropertyMapping<ArticleResource, Article>
    {
        public ArticlePropertyMapping()
            : base(new Dictionary<string, List<MappedProperty>>(StringComparer.OrdinalIgnoreCase)
            {
                [nameof(ArticleResource.Title)] = new List<MappedProperty>
            {
                new MappedProperty{ Name = nameof(Article.Title), Revert = false}
            },
                [nameof(ArticleResource.Author)] = new List<MappedProperty>
            {
                new MappedProperty{ Name = nameof(Article.Author), Revert = false}
            }
            })
        {
        }
    }
}
