using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace WYBlog.Dtos
{
    public class CreateOrEditTagDto
    {
        /// <summary>
        /// 标签名称
        /// </summary>
        public string TagName { get; set; }

        /// <summary>
        /// 标签Key
        /// </summary>
        public string TagKey { get; set; }
    }
}
