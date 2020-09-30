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

namespace WYBlog.AppServices.Blog
{
    public class CategoryService : ApplicationService, ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<CategoryDto> CreateAsync(CreateOrEditCategoryDto input)
        {
            var categoryEntity = ObjectMapper.Map<CreateOrEditCategoryDto, Category>(input);
            var resultEntity = await _repository.InsertAsync(categoryEntity, true);

            return ObjectMapper.Map<Category, CategoryDto>(resultEntity);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(x => x.Id == id);
        }

        public async Task<CategoryDto> GetAsync(int id)
        {
            var categoryEntity = await _repository.GetAsync(x => x.Id == id);
            return ObjectMapper.Map<Category, CategoryDto>(categoryEntity);
        }

        public async Task<CategoryDto> GetByKeyAsync(string key)
        {
            var categoryEntity = await _repository.GetAsync(x => x.CategoryKey == key);
            return ObjectMapper.Map<Category, CategoryDto>(categoryEntity);
        }

        public async Task<PagedResultDto<CategoryDto>> GetPagedListAsync(QueryCategoryDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Category.CategoryName);
            }

            var categoryList = await _repository.GetPagedListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );

            var totalCount = await AsyncExecuter.CountAsync(
                _repository.WhereIf(!input.Filter.IsNullOrWhiteSpace(), x => x.CategoryName.Contains(input.Filter)
                )
            );

            return new PagedResultDto<CategoryDto>(
                totalCount,
                ObjectMapper.Map<List<Category>, List<CategoryDto>>(categoryList)
            );
        }

        public async Task UpdateAsync(int id, CreateOrEditCategoryDto input)
        {
            var categoryEntity = await _repository.GetAsync(x => x.Id == id);
            categoryEntity = ObjectMapper.Map<CreateOrEditCategoryDto, Category>(input);

            await _repository.UpdateAsync(categoryEntity);
        }
    }
}