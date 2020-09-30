using Volo.Abp.Application.Dtos;

namespace WYBlog.Dtos
{
    public class QueryGuestBookDto : PagedAndSortedResultRequestDto
    {
        public int ParentId { get; set; }
        public string Filter { get; set; }
    }
}