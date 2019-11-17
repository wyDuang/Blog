using Blog.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Blog.Infrastructure.Extensions
{
    public static class ModelBuilderExtenstions
    {
        /// <summary>
        /// 批量注入DbSet<>();
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void MappingEntityTypes(this ModelBuilder modelBuilder)
        {
            var types = Assembly.GetAssembly(typeof(IEntity)).GetTypes();

            //var list = types?.Where(t => t.IsClass && !t.IsGenericType && !t.IsAbstract && t.GetInterfaces().Any(m => m.IsAssignableFrom(typeof(IEntity)))).ToList();
            //if (list != null && list.Any())
            //{
            //    list.ForEach(t =>
            //    {
            //        modelBuilder.Model.AddEntityType(t);
            //    });
            //}

            foreach (var type in types)
            {
                if (!type.IsClass || !type.IsAbstract)
                {
                    continue;
                }

                var isEntity = typeof(IEntity).IsAssignableFrom(type);
                if (!isEntity)
                {
                    continue;
                }
                else
                {
                    modelBuilder.Entity(type);
                }
            }
        }
    }
}
