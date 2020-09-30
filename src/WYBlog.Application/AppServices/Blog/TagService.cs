using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Uow;
using WYBlog.Dtos;
using WYBlog.Entities;
using WYBlog.IAppServices;
using WYBlog.IRepository;

namespace WYBlog.AppServices
{
    public class TagService : ApplicationService, ITagService
    {
        private readonly ITagRepository _repository;

        public TagService(ITagRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 分页查询标签列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<TagDto>> GetPagedListAsync(QueryTagDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Tag.TagName);
            }

            var tagList = await _repository.GetPagedListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );

            var totalCount = await AsyncExecuter.CountAsync(
                _repository.WhereIf(!input.Filter.IsNullOrWhiteSpace(), x => x.TagName.Contains(input.Filter)
                )
            );

            return new PagedResultDto<TagDto>(
                totalCount,
                ObjectMapper.Map<List<Tag>, List<TagDto>>(tagList)
            );
        }

        /// <summary>
        /// 通过Id获取标签
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TagDto> GetAsync(int id)
        {
            var tagEntity = await _repository.GetAsync(x => x.Id == id);
            return ObjectMapper.Map<Tag, TagDto>(tagEntity);
        }

        /// <summary>
        /// 通过Key获取标签
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TagDto> GetByKeyAsync(string key)
        {
            var tagEntity = await _repository.GetAsync(x => x.TagKey == key);
            return ObjectMapper.Map<Tag, TagDto>(tagEntity);
        }

        /// <summary>
        /// 新增标签
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<TagDto> CreateAsync(CreateOrEditTagDto input)
        {
            var tagEntity = ObjectMapper.Map<CreateOrEditTagDto, Tag>(input);
            var resultEntity = await _repository.InsertAsync(tagEntity, true);

            return ObjectMapper.Map<Tag, TagDto>(resultEntity);
        }

        /// <summary>
        /// 编辑标签
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateAsync(int id, CreateOrEditTagDto input)
        {
            var tagEntity = await _repository.GetAsync(x => x.Id == id);

            //ObjectMapper.Map(input, tagEntity);

            tagEntity.TagName = input.TagName;
            tagEntity.TagKey = input.TagKey;

            await _repository.UpdateAsync(tagEntity);
        }

        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(x => x.Id == id);
        }
    }
}