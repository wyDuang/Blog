using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.ResultModel
{
    public class DataResult : BaseResult
    {
        public dynamic Data { get; set; }
    }
}
