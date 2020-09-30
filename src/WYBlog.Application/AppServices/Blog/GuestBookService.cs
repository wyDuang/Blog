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
    public class GuestBookService : ApplicationService, IGuestBookService
    {
        private readonly IGuestBookRepository _repository;

        public GuestBookService(IGuestBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<GuestBookDto> CreateAsync(CreateOrEditGuestBookDto input)
        {
            var guestBookEntity = ObjectMapper.Map<CreateOrEditGuestBookDto, GuestBook>(input);
            var resultEntity = await _repository.InsertAsync(guestBookEntity, true);

            return ObjectMapper.Map<GuestBook, GuestBookDto>(resultEntity);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(x => x.Id == id);
        }

        public async Task<GuestBookDto> GetAsync(int id)
        {
            var guestBookEntity = await _repository.GetAsync(x => x.Id == id);
            return ObjectMapper.Map<GuestBook, GuestBookDto>(guestBookEntity);
        }

        public async Task<PagedResultDto<GuestBookDto>> GetPagedListAsync(QueryGuestBookDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(GuestBook.CreateTime);
            }

            var list = await _repository.GetPagedListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.ParentId,
                input.Filter
            );

            var totalCount = await AsyncExecuter.CountAsync(
                _repository.WhereIf(!input.Filter.IsNullOrWhiteSpace(), x => x.Email.Contains(input.Filter) && x.ParentId == input.ParentId
                )
            );

            return new PagedResultDto<GuestBookDto>(
                totalCount,
                ObjectMapper.Map<List<GuestBook>, List<GuestBookDto>>(list)
            );
        }

        public async Task UpdateAsync(int id, CreateOrEditGuestBookDto input)
        {
            var guestBookEntity = await _repository.GetAsync(x => x.Id == id);
            guestBookEntity = ObjectMapper.Map<CreateOrEditGuestBookDto, GuestBook>(input);

            await _repository.UpdateAsync(guestBookEntity);
        }
    }
}
