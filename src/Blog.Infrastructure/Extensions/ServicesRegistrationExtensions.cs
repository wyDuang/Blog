using AutoMapper;
using Blog.Core.Interfaces;
using Blog.Infrastructure.AutoMapper;
using Blog.Infrastructure.Database;
using Blog.Infrastructure.Repositories;
using Blog.Infrastructure.Resources;
using Blog.Infrastructure.Resources.PropertyMappings;
using Blog.Infrastructure.Resources.Validators;
using Blog.Infrastructure.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Linq;
using System.Reflection;

namespace Blog.Infrastructure.Extensions
{
    public static class ServicesRegistrationExtensions
    {
        public static void AddMyServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperConfig));
            AutoMapperConfig.RegisterMappings();

            var types = Assembly.GetExecutingAssembly().GetTypes().Where(p => p.BaseType.GetInterfaces().Any(x => x == typeof(IValidator)));
            foreach (var type in types)
            {
                if (type.BaseType != null)
                {
                    var genericType = typeof(IValidator<>).MakeGenericType(type.BaseType.GenericTypeArguments[0]);
                    services.AddTransient(genericType, type);
                }
            }

            //services.TryAddTransient<IValidator<ArticleAddResource>, ArticleAddOrUpdateResourceValidator<ArticleAddResource>>();
            //services.TryAddTransient<IValidator<ArticleUpdateResource>, ArticleAddOrUpdateResourceValidator<ArticleUpdateResource>>();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpContextAccessor();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUrlHelper>(factory =>
            {
                var actionContext = factory.GetService<IActionContextAccessor>().ActionContext;
                return new UrlHelper(actionContext);
            });

            var propertyMappingContainer = new PropertyMappingContainer();
            propertyMappingContainer.Register<ArticlePropertyMapping>();

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
