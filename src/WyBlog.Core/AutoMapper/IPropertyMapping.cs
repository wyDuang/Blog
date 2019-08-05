using System;
using System.Collections.Generic;
using System.Text;

namespace WyBlog.Core.AutoMapper
{
    public interface IPropertyMapping
    {
        Dictionary<string, List<MappedProperty>> MappingDictionary { get; }
    }
}
