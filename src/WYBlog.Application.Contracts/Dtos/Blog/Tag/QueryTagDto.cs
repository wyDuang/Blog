using Volo.Abp.Application.Dtos;

namespace WYBlog.Dtos
{
    public class QueryTagDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}