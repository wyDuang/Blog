using Volo.Abp.Application.Dtos;

namespace WYBlog.Dtos
{
    public class QueryFriendLinkDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}