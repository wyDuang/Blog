using Blog.Core.SettingModels;
using Blog.Infrastructure.Routes;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Extensions
{
    public static class RouteBuilderExtensions
    {
        public static IRouteBuilder MapDomainRoute(this IRouteBuilder routeBuilder, string domain, string area, string controller)
        {
            try
            {
                if (string.IsNullOrEmpty(domain))
                {
                    throw new ArgumentNullException("domain can not be null");
                }
                //string.IsNullOrEmpty(area) ||  //去掉该判断,不限制仅限区域使用配置
                if (string.IsNullOrEmpty(controller))
                {
                    throw new ArgumentNullException("controller can not be null");
                }
                var inlineConstraintResolver = routeBuilder
                    .ServiceProvider
                    .GetRequiredService<IInlineConstraintResolver>();

                string template = "";

                RouteValueDictionary defaults = new RouteValueDictionary
                {
                    { "area", area },
                    { "controller", string.IsNullOrEmpty(controller) ? "home" : controller },
                    { "action", "index" }
                };

                    RouteValueDictionary constrains = new RouteValueDictionary
                {
                    { "area", area },
                    { "controller", controller }
                };

                template += "{action}/{id?}";//路径规则中不再包含控制器信息，但是上面通过constrains限定了查找时所要求的控制器名称
                routeBuilder.Routes.Add(new SubDomainRouter(routeBuilder.DefaultHandler, domain, template, defaults, constrains, inlineConstraintResolver));

                return routeBuilder;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void MapDomainRoute(this IRouteBuilder routeBuilder, List<SubDomain> listDomains)
        {
            try
            {
                if (listDomains == null || listDomains.Count <= 0)
                {
                    return;
                }

                foreach (SubDomain domain in listDomains)
                {
                    MapDomainRoute(routeBuilder, domain.Domain, domain.AreaName, domain.Controller);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
