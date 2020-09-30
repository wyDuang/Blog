using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using WYBlog.Dtos;
using WYBlog.Entities;
using WYBlog.IAppServices;
using WYBlog.IRepository;

namespace WYBlog.AppServices
{
    public class FriendLinkService : ApplicationService, IFriendLinkService
    {
        private readonly IFriendLinkRepository _repository;

        public FriendLinkService(IFriendLinkRepository repository)
        {
            _repository = repository;
        }

        public async Task<FriendLinkDto> CreateAsync(CreateOrEditFriendLinkDto input)
        {
            var friendLinkEntity = ObjectMapper.Map<CreateOrEditFriendLinkDto, FriendLink>(input);
            var resultEntity = await _repository.InsertAsync(friendLinkEntity, true);

            return ObjectMapper.Map<FriendLink, FriendLinkDto>(resultEntity);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(x => x.Id == id);
        }

        public async Task<FriendLinkDto> GetAsync(int id)
        {
            var friendLinkEntity = await _repository.GetAsync(x => x.Id == id);
            return ObjectMapper.Map<FriendLink, FriendLinkDto>(friendLinkEntity);
        }

        public async Task<PagedResultDto<FriendLinkDto>> GetPagedListAsync(QueryFriendLinkDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(FriendLink.CreateTime);
            }

            var list = await _repository.GetPagedListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );

            var totalCount = await AsyncExecuter.CountAsync(
                _repository.WhereIf(!input.Filter.IsNullOrWhiteSpace(), x => x.Title.Contains(input.Filter)
                )
            );

            return new PagedResultDto<FriendLinkDto>(
                totalCount,
                ObjectMapper.Map<List<FriendLink>, List<FriendLinkDto>>(list)
            );
        }

        public async Task UpdateAsync(int id, CreateOrEditFriendLinkDto input)
        {
            var friendLinkEntity = await _repository.GetAsync(x => x.Id == id);
            friendLinkEntity = ObjectMapper.Map<CreateOrEditFriendLinkDto, FriendLink>(input);

            await _repository.UpdateAsync(friendLinkEntity);
        }
    }
}
