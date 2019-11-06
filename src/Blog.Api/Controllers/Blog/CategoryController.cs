using AutoMapper;
using Blog.Core.BaseModels;
using Blog.Core.Entities;
using Blog.Core.Interfaces;
using Blog.Core.QueryParameters;
using Blog.Infrastructure.Extensions;
using Blog.Infrastructure.Resources;
using Blog.Infrastructure.Resources.Hateoas;
using Blog.Infrastructure.ResultModel;
using Blog.Infrastructure.Swagger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Blog.Api.Controllers
{
    [Authorize]
    [Route("categories")]
    public class CategoryController : BaseController
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(
            ICategoryRepository categoryRepository,
            IUnitOfWork unitOfWork,
            ILogger<CategoryController> logger,
            IMapper mapper,
            IUrlHelper urlHelper,
            ITypeHelperService typeHelperService,
            IPropertyMappingContainer propertyMappingContainer)
            : base(unitOfWork, logger, mapper, urlHelper, typeHelperService, propertyMappingContainer)
        {
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// 分页获取文章
        /// </summary>
        [HttpGet(Name = "GetCategories")]
        public async Task<IActionResult> Gets(CategoryParameter parameter, [FromHeader(Name = "Accept")] string mediaType)
        {
            if (!_propertyMappingContainer.ValidateMappingExistsFor<CategoryResource, Category>(parameter.OrderBy))
                return BadRequest("找不到要排序的字段。");

            if (!_typeHelperService.TypeHasProperties<ArticleResource>(parameter.Fields))
                return BadRequest("字段不存在。");

            var pagedList = await _categoryRepository.GetPageListAsync(parameter, _propertyMappingContainer.Resolve<CategoryResource, Category>());
            var categoryResources = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryResource>>(pagedList);

            if (mediaType == "application/vnd.wyduang.hateoas+json")
            {
                var meta = new
                {
                    pagedList.Pagination.PageSize,
                    pagedList.Pagination.PageIndex,
                    pagedList.TotalItemsCount,
                    pagedList.PageCount
                };

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(meta));

                var links = CreateLinks(parameter, pagedList.HasPrevious, pagedList.HasNext);

                var shapedResources = categoryResources.ToDynamicIEnumerable(parameter.Fields);
                var shapedWithLinks = shapedResources.Select(x =>
                {
                    var dict = x as IDictionary<string, object>;
                    var links = CreateLink((int)dict["Id"], parameter.Fields);
                    dict.Add("links", links);
                    return dict;
                });

                var result = new
                {
                    value = shapedWithLinks,
                    links
                };
                return Ok(result);
            }
            else
            {
                var previousPageLink = pagedList.HasPrevious ? CreateUri(parameter, PaginationUriType.PreviousPage) : null;
                var nextPageLink = pagedList.HasNext ? CreateUri(parameter, PaginationUriType.NextPage) : null;

                var meta = new
                {
                    pagedList.TotalItemsCount,
                    pagedList.Pagination.PageSize,
                    pagedList.Pagination.PageIndex,
                    pagedList.PageCount,
                    previousPageLink,
                    nextPageLink
                };

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(meta));

                return Ok(categoryResources.ToDynamicIEnumerable(parameter.Fields));
            }
        }

        /// <summary> 
        /// 根据ID获取一个分类
        /// </summary>
        //[DisableCors]
        [HttpGet("{id}", Name = "GetCategory")]
        public async Task<IActionResult> Get(int id, string fields = null)
        {
            if (!_typeHelperService.TypeHasProperties<CategoryResource>(fields))
                return BadRequest("字段不存在。");

            var category = await _categoryRepository.GetAsync(id);
            if (category == null) 
                return NotFound();

            var categoryResource = _mapper.Map<Category, CategoryResource>(category);

            var links = CreateLink(id, fields);
            var result = categoryResource.ToDynamic(fields) as IDictionary<string, object>;
            result.Add("links", links);

            return Ok(result);
        }

        /// <summary>
        /// 创建分类
        /// </summary>
        [HttpPost(Name = "CreateCategory")]
        //[RequestHeaderMatchingMediaType("Content-Type", new[] { "application/vnd.wyduang.article.create+json" })]
        //[RequestHeaderMatchingMediaType("Accept", new[] { "application/vnd.wyduang.article.display+json" })]
        public async Task<IActionResult> Post([FromBody] CategoryAddResource categoryAddResource)
        {
            if (categoryAddResource == null)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                return new MyUnprocessableEntityObjectResult(ModelState);
            }

            var newCategory = _mapper.Map<CategoryAddResource, Category>(categoryAddResource);
            newCategory.CategoryKey = newCategory.CategoryName.GetKeyName();

            _categoryRepository.Add(newCategory);

            if (!await _unitOfWork.SaveAsync())
                throw new Exception("保存失败！");

            var resultResource = _mapper.Map<Category, CategoryResource>(newCategory);

            var links = CreateLink(newCategory.Id);
            var linkedArticleResource = resultResource.ToDynamic() as IDictionary<string, object>;
            linkedArticleResource.Add("links", links);

            return CreatedAtRoute("GetCategory", new { id = linkedArticleResource["Id"] }, linkedArticleResource);
        }

        /// <summary>
        /// 判断是存在此分类
        /// </summary>
        [HttpPost("{id}", Name = "IsExistCategory")]
        public async Task<IActionResult> IsExist(string key)
        {
            var categories = await _categoryRepository.GetListAsync(x=>x.CategoryKey == key);
            if (categories == null) return NoContent();

            return StatusCode(StatusCodes.Status409Conflict);
        }

        [HttpDelete("{id}", Name = "DeleteCategory")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryRepository.GetAsync(id);
            if (category == null) return NotFound();

            _categoryRepository.Delete(category);

            if (!await _unitOfWork.SaveAsync())
            {
                throw new Exception($"执行 {id} 删除时保存失败。");
            }
            return NoContent();
        }

        [HttpPut("{id}", Name = "UpdateCategory")]
        //[RequestHeaderMatchingMediaType("Content-Type", new[] { "application/vnd.wyduang.article.update+json" })]
        public async Task<IActionResult> Update(int id, [FromBody] ArticleUpdateResource articleUpdateResource)
        {
            if (articleUpdateResource == null) return BadRequest();
            if (!ModelState.IsValid)
                return new UnprocessableEntityObjectResult(ModelState);

            var article = await _categoryRepository.GetAsync(id);
            if (article == null) return NotFound();

            _mapper.Map(articleUpdateResource, article);

            if (!await _unitOfWork.SaveAsync())
            {
                throw new Exception($"更新类别 {id} 时保存失败。");
            }
            return NoContent();
        }

        private string CreateUri(CategoryParameter parameters, PaginationUriType uriType)
        {
            switch (uriType)
            {
                case PaginationUriType.PreviousPage:
                    var previousParameters = new
                    {
                        pageIndex = parameters.PageIndex - 1,
                        pageSize = parameters.PageSize,
                        orderBy = parameters.OrderBy,
                        fields = parameters.Fields,
                        name = parameters.Name
                    };
                    return _urlHelper.Link("GetCategories", previousParameters);
                case PaginationUriType.NextPage:
                    var nextParameters = new
                    {
                        pageIndex = parameters.PageIndex + 1,
                        pageSize = parameters.PageSize,
                        orderBy = parameters.OrderBy,
                        fields = parameters.Fields,
                        name = parameters.Name
                    };
                    return _urlHelper.Link("GetCategories", nextParameters);
                default:
                    var currentParameters = new
                    {
                        pageIndex = parameters.PageIndex,
                        pageSize = parameters.PageSize,
                        orderBy = parameters.OrderBy,
                        fields = parameters.Fields,
                        name = parameters.Name
                    };
                    return _urlHelper.Link("GetCategories", currentParameters);
            }
        }

        private IEnumerable<LinkResource> CreateLink(int id, string fields = null)
        {
            var links = new List<LinkResource>();

            if (string.IsNullOrWhiteSpace(fields))
                links.Add(new LinkResource(_urlHelper.Link("GetCategory", new { id }), "self", "GET"));
            else
                links.Add(new LinkResource(_urlHelper.Link("GetCategory", new { id, fields }), "self", "GET"));
            links.Add(new LinkResource(_urlHelper.Link("DeleteCategory", new { id }), "delete_category", "DELETE"));

            return links;
        }

        private IEnumerable<LinkResource> CreateLinks(CategoryParameter parameter, bool hasPrevious, bool hasNext)
        {
            var links = new List<LinkResource>
            {
                new LinkResource( CreateUri(parameter, PaginationUriType.CurrentPage), "self", "GET")
            };

            if (hasPrevious)
            {
                links.Add(new LinkResource(CreateUri(parameter, PaginationUriType.PreviousPage), "previous_page", "GET"));
            }
            if (hasNext)
            {
                links.Add(new LinkResource(CreateUri(parameter, PaginationUriType.NextPage), "next_page", "GET"));
            }

            return links;
        }
    }
}