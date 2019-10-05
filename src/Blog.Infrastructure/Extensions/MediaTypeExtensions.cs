using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Infrastructure.Extensions
{
    public static class MediaTypeExtensions
    {
        public static void AddMediaTypes(this IServiceCollection services)
        {
            services.Configure<MvcOptions>(options =>
            {
                var jsonInputFormatter = options.OutputFormatters.OfType<JsonInputFormatter>().FirstOrDefault();
                if (jsonInputFormatter != null)
                {
                    InputMediaTypes.ForEach(x => jsonInputFormatter.SupportedMediaTypes.Add(x));
                }
                var jsonOutputFormatter = options.OutputFormatters.OfType<JsonInputFormatter>().FirstOrDefault();
                if (jsonOutputFormatter != null)
                {
                    OutputMediaTypes.ForEach(x => jsonOutputFormatter.SupportedMediaTypes.Add(x));
                }
            });
        }

        public static readonly List<string> InputMediaTypes = new List<string>
        {
            // Country
            "application/vnd.solenovex.country.create+json",
            "application/vnd.solenovex.country.update+json",
            "application/vnd.solenovex.countrywithcontinent.create+json",
            // Product
            "application/vnd.solenovex.product.create+json",
            "application/vnd.solenovex.product.update+json",
            "application/vnd.solenovex.productwithcontinent.create+json"
        };

        public static readonly List<string> OutputMediaTypes = new List<string>
        {
            "application/vnd.solenovex.hateoas+json",
            // Country
            "application/vnd.solenovex.country.display+json",
            "application/vnd.solenovex.countrywithcontinent.display+json",
            // Product
            "application/vnd.solenovex.product.display+json",
            "application/vnd.solenovex.productwithcontinent.display+json"
        };
    }
}
