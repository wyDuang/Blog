﻿using Volo.Abp.Modularity;

namespace WYBlog
{
    [DependsOn(
        typeof(BlogDomainSharedModule)
        )]
    public class BlogApplicationContractsModule : AbpModule
    {
    }
}