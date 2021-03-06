﻿using Volo.Abp.Application.Dtos;

namespace WYBlog.Dtos
{
    public class CategoryDto : EntityDto<int>
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// url的key值
        /// </summary>
        public string CategoryKey { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}