using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Routes
{
    public class SubDomainRouter: RouteBase
    {
        private readonly IRouter _target;
        private readonly string _subDomain;

        public SubDomainRouter(
         IRouter target,
         string subDomain,//当前路由规则绑定的二级域名
         string routeTemplate,
         RouteValueDictionary defaults,
         RouteValueDictionary constrains,
         IInlineConstraintResolver inlineConstraintResolver)
            : base(routeTemplate, subDomain, inlineConstraintResolver, defaults, constrains, new RouteValueDictionary(null))
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }
            if (subDomain == null)
            {
                throw new ArgumentNullException(nameof(subDomain));
            }
            _subDomain = subDomain;
            _target = target;
        }

        public override Task RouteAsync(RouteContext context)
        {
            string domain = context.HttpContext.Request.Host.Host;//获取当前请求域名，然后跟_subDomain比较，如果不想等，直接忽略

            if (string.IsNullOrEmpty(domain) || string.Compare(_subDomain, domain) != 0)
            {
                return Task.CompletedTask;
            }

            //如果域名匹配，再去验证访问路径是否匹配

            return base.RouteAsync(context);

        }

        protected override Task OnRouteMatched(RouteContext context)
        {
            context.RouteData.Routers.Add(_target);
            return _target.RouteAsync(context);
        }

        protected override VirtualPathData OnVirtualPathGenerated(VirtualPathContext context)
        {
            return _target.GetVirtualPath(context);
        }
    }
}
