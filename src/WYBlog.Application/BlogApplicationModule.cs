using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.AspNetCore.Authentication.JwtBearer;
using Volo.Abp.AutoMapper;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.RabbitMQ;
using WYBlog.Configurations;

namespace WYBlog
{
    [DependsOn(
        typeof(BlogDomainModule),
        typeof(BlogApplicationContractsModule),
        typeof(AbpCachingModule),
        typeof(AbpAutoMapperModule),
        typeof(AbpEventBusRabbitMqModule),
        typeof(AbpPermissionManagementApplicationModule),
        typeof(AbpAspNetCoreAuthenticationJwtBearerModule)
        )]
    public class BlogApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<BlogApplicationModule>();
                //options.AddProfile<BlogApplicationAutoMapperProfile>(validate: true);
            });

            ConfigureCaching(context.Services);
            ConfigureRabbitMQ();
        }

        /// <summary>
        /// Redis
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureCaching(IServiceCollection services)
        {
            if (AppSettings.Caching.IsOpen)
            {
                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = AppSettings.Caching.RedisConnectionString;
                });

                var csredis = new CSRedis.CSRedisClient(AppSettings.Caching.RedisConnectionString);
                RedisHelper.Initialization(csredis);

                services.AddSingleton<IDistributedCache>(new CSRedisCache(RedisHelper.Instance));
            }
        }

        /// <summary>
        /// EventBus - RabbitMq
        /// </summary>
        private void ConfigureRabbitMQ()
        {
            Configure<AbpDistributedEntityEventOptions>(options =>
            {
                options.AutoEventSelectors.AddAll();
            });

            Configure<AbpRabbitMqOptions>(options =>
            {
                options.Connections.Default.UserName = AppSettings.RabbitMQ.Connections.Default.Username;
                options.Connections.Default.Password = AppSettings.RabbitMQ.Connections.Default.Password;
                options.Connections.Default.HostName = AppSettings.RabbitMQ.Connections.Default.HostName;
                options.Connections.Default.Port = AppSettings.RabbitMQ.Connections.Default.Port;
            });

            Configure<AbpRabbitMqEventBusOptions>(options =>
            {
                options.ClientName = AppSettings.RabbitMQ.EventBus.ClientName;
                options.ExchangeName = AppSettings.RabbitMQ.EventBus.ExchangeName;
            });
        }
    }
}