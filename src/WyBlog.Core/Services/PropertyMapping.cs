using System;
using System.Collections.Generic;
using System.Text;
using WyBlog.Core.Models;

namespace WyBlog.Core.AutoMapper
{
    public abstract class PropertyMapping<TSource, TDestination> : IPropertyMapping
        where TDestination : IBaseEntity
    {
        public Dictionary<string, List<MappedProperty>> MappingDictionary { get; }

        protected PropertyMapping(Dictionary<string, List<MappedProperty>> mappingDictionary)
        {
            MappingDictionary = mappingDictionary;
            MappingDictionary[nameof(IBaseEntity.Id)] = new List<MappedProperty>
            {
                new MappedProperty { Name = nameof(IBaseEntity.Id), Revert = false}
            };
        }
    }
}
