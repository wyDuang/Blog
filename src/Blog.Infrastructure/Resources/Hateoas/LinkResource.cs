using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Resources.Hateoas
{
    public class LinkResource
    {
        public LinkResource(string href, string rel, string method)
        {
            Href = href;
            Rel = rel;
            Method = method;
        }

        public string Href { get; set; }
        public string Rel { get; set; }
        public string Method { get; set; }
    }
}
