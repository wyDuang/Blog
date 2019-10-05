using Blog.Core.BaseModels;
using System.Collections.Generic;

namespace Blog.Core.Interfaces
{
    public interface IPropertyMapping
    {
        Dictionary<string, List<MappedProperty>> MappingDictionary { get; }
    }
}