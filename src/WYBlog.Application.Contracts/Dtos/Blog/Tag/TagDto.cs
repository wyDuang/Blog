using Volo.Abp.Application.Dtos;

namespace WYBlog.Dtos
{
    public class TagDto: EntityDto<int>
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