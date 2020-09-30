using Volo.Abp.Application.Dtos;

namespace WYBlog.Dtos
{
    public class QueryCategoryDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}