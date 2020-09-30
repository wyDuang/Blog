using Volo.Abp.Application.Dtos;

namespace WYBlog.Dtos
{
    public class QueryArticleDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}