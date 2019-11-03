using Blog.Core.BaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.QueryParameters
{
    public class FriendLinkParameter : PaginationBase
    {
        public string Title { get; set; }
    }
}
