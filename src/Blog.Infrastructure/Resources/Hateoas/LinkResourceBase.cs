using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Resources.Hateoas
{
    public abstract class LinkResourceBase
    {
        public List<LinkResource> Links { get; set; } = new List<LinkResource>();
    }
}
