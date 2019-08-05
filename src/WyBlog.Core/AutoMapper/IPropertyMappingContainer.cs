using System;
using System.Collections.Generic;
using System.Text;
using WyBlog.Core.Models;

namespace WyBlog.Core.AutoMapper
{
    public interface IPropertyMappingContainer
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void Register<T>() where T : IPropertyMapping, new();

        IPropertyMapping Resolve<TSource, TDestination>() where TDestination : IEntity;

        bool ValidateMappingExistsFor<TSource, TDestination>(string fields) where TDestination : IEntity;
    }
}
