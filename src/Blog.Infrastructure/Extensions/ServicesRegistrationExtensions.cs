using Blog.Core.Interfaces;
using Blog.Infrastructure.Database;
using Blog.Infrastructure.Repositories;
using Blog.Infrastructure.Resources.PropertyMappings;
using Blog.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

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

            services.TryAddScoped(typeof(IRepositoryBase<>), typeof(EFRepository<>));

            services.TryAddScoped<IUnitOfWork, UnitOfWork>();

            services.TryAddScoped<IUserRepository, UserRepository>();
            services.TryAddScoped<IUserRoleRepository, UserRoleRepository>();

            services.TryAddScoped<IArticleRepository, ArticleRepository>();
            services.TryAddScoped<IArticleTagRepository, ArticleTagRepository>();
            services.TryAddScoped<ICategoryRepository, CategoryRepository>();
            services.TryAddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IFriendLinkRepository, FriendLinkRepository>();

            services.AddHttpClient("github", config =>
            {
                config.BaseAddress = new Uri("https://github.com/");
                config.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36");
            });
            services.AddHttpClient("translate", config =>
            {
                config.BaseAddress = new Uri("https://translate.google.cn");
                config.DefaultRequestHeaders.Add("Accept", "*/*");
                config.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36");
                config.DefaultRequestHeaders.Referrer = new Uri("https://translate.google.cn");
            });
            services.AddHttpClient();
        }
    }
}
