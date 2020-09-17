using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace WYBlog.EntityFrameworkCore
{
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// 批量注入DbSet<>();
        /// </summary>
        /// <param name="builder"></param>
        public static void ConfigureMappingEntityTypes(this ModelBuilder builder)
        {
            var types = Assembly.GetAssembly(typeof(IEntity)).GetTypes();

            foreach (var type in types)
            {
                if (!type.IsClass || !type.IsAbstract)
                    continue;

                var isEntity = typeof(IEntity).IsAssignableFrom(type);
                if (!isEntity)
                    continue;
                else
                    builder.Entity(type);
            }
        }
    }
}