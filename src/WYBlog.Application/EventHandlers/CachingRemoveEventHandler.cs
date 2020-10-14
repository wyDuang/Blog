using Microsoft.Extensions.Caching.Distributed;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace WYBlog
{
    public class CachingRemoveEventHandler : IDistributedEventHandler<CachingRemoveEventData>, ITransientDependency
    {
        public IDistributedCache Cache { get; set; }

        public async Task HandleEventAsync(CachingRemoveEventData eventData)
        {
            await Cache.RemoveAsync(eventData.Key);
        }
    }
}