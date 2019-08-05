using System;
using System.Collections.Generic;
using System.Text;

namespace WyBlog.Core.AutoMapper
{
    public class MappedProperty
    {
        public string Name { get; set; }
        /// <summary>
        /// 还原
        /// </summary>
        public bool Revert { get; set; }
    }
}
