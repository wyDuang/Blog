using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;

namespace WYBlog
{
    public static class BlogDbContextModelCreatingExtensions
    {
        public static void ConfigureBlog(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));



        }
    }
}
