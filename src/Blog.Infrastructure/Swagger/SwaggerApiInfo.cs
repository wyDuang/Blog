using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.OpenApi.Models;

namespace Blog.Infrastructure.Swagger
{
    public class SwaggerApiInfo
    {
        public string UrlPrefix { get; set; }

        public string Name { get; set; }

        public OpenApiInfo OpenApiInfo { get; set; }
    }
}
