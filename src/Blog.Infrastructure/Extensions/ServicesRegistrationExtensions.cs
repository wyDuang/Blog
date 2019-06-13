using Blog.Core.Interfaces;
using Blog.Infrastructure.Database;
using Blog.Infrastructure.Repositories;
using Blog.Infrastructure.Resources.PropertyMappings;
using Blog.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Blog.Infrastructure.Extensions
{
    public static class ServicesRegistrationExtensions
    {
        public static void AddMyServices(this IServiceCollection services)
        {
            //services.TryAddTransient<IValidator<CityAddResource>, CityAddOrUpdateResourceValidator<CityAddResource>>();
            //services.TryAddTransient<IValidator<CityUpdateResource>, CityUpdateResourceValidator>();
            //services.TryAddTransient<IValidator<CountryAddResource>, CountryAddResourceValidator>();

            //services.TryAddTransient<IValidator<ProductAddResource>, ProductAddOrUpdateResourceValidator<ProductAddResource>>();
            //services.TryAddTransient<IValidator<ProductUpdateResource>, ProductAddOrUpdateResourceValidator<ProductUpdateResource>>();

            var propertyMappingContainer = new PropertyMappingContainer();
            propertyMappingContainer.Register<ArticlePropertyMapping>();
            //propertyMappingContainer.Register<ProductPropertyMapping>();

            services.TryAddSingleton<IPropertyMappingContainer>(propertyMappingContainer);
            services.TryAddTransient<ITypeHelperService, TypeHelperService>();

            services.TryAddScoped<IUnitOfWork, UnitOfWork>();
            services.TryAddScoped(typeof(IRepositoryBase<>), typeof(EFRepository<>));

            services.TryAddScoped<IArticleRepository, ArticleRepository>();
            services.TryAddScoped<IArticleTagRepository, ArticleTagRepository>();
            services.TryAddScoped<ICategoryRepository, CategoryRepository>();
            services.TryAddScoped<ITagRepository, TagRepository>();
        }
    }
}
