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
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Api.Controllers.Blog
{
    [Authorize]
    [Route("tags")]
    [ApiExplorerSettings(GroupName = ApiVersionConsts.GroupName_v1)]
    public class TagController : BaseController
    {
        private readonly ITagRepository _tagRepository;
        public TagController(
            ITagRepository tagRepository,
            IUnitOfWork unitOfWork,
            ILogger<TagController> logger,
            IMapper mapper,
            IUrlHelper urlHelper,
            ITypeHelperService typeHelperService,
            IPropertyMappingContainer propertyMappingContainer)
            : base(unitOfWork, logger, mapper, urlHelper, typeHelperService, propertyMappingContainer)
        {
            _tagRepository = tagRepository;
        }

        /// <summary>
        /// 分页获取标签
        /// </summary>
        [HttpGet(Name = "GetTags")]
        public async Task<IActionResult> Gets(TagParameter parameter, [FromHeader(Name = "Accept")] string mediaType)
        {
            if (!_propertyMappingContainer.ValidateMappingExistsFor<TagResource, Tag>(parameter.OrderBy))
                return BadRequest("找不到要排序的字段。");

            if (!_typeHelperService.TypeHasProperties<TagResource>(parameter.Fields))
                return BadRequest("字段不存在。");

            var pagedList = await _tagRepository.GetPageListAsync(parameter, _propertyMappingContainer.Resolve<TagResource, Tag>());
            var tagResources = _mapper.Map<IEnumerable<Tag>, IEnumerable<TagResource>>(pagedList);

            if (mediaType == "application/vnd.wyduang.hateoas+json")
            {
                var meta = new
                {
                    pagedList.Pagination.PageSize,
                    pagedList.Pagination.PageIndex,
                    pagedList.TotalItemsCount,
                    pagedList.PageCount
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(meta, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }));

                var links = CreateLinks(parameter, pagedList.HasPrevious, pagedList.HasNext);

                var shapedResources = tagResources.ToDynamicIEnumerable(parameter.Fields);
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

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(meta, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }));

                return Ok(tagResources.ToDynamicIEnumerable(parameter.Fields));
            }
        }

        /// <summary> 
        /// 根据ID获取一个标签
        /// </summary>
        //[DisableCors]
        [HttpGet("{id}", Name = "GetTag")]
        public async Task<IActionResult> Get(int id, string fields = null)
        {
            if (!_typeHelperService.TypeHasProperties<TagResource>(fields))
                return BadRequest("字段不存在。");

            var tag = await _tagRepository.GetAsync(id);
            if (tag == null)
                return NotFound();

            var tagResource = _mapper.Map<Tag, TagResource>(tag);

            var links = CreateLink(id, fields);
            var result = tagResource.ToDynamic(fields) as IDictionary<string, object>;
            result.Add("links", links);

            return Ok(result);
        }

        /// <summary>
        /// 创建标签
        /// </summary>
        [HttpPost(Name = "CreateTag")]
        //[RequestHeaderMatchingMediaType("Content-Type", new[] { "application/vnd.wyduang.article.create+json" })]
        //[RequestHeaderMatchingMediaType("Accept", new[] { "application/vnd.wyduang.article.display+json" })]
        public async Task<IActionResult> Post([FromBody] TagResource AddResource)
        {
            if (AddResource == null) return BadRequest();

            if (!ModelState.IsValid)
                return new MyUnprocessableEntityObjectResult(ModelState);

            var newTag = _mapper.Map<TagResource, Tag>(AddResource);
            _tagRepository.Add(newTag);

            if (!await _unitOfWork.SaveAsync())
                throw new Exception("保存失败！");

            var resultResource = _mapper.Map<Tag, TagResource>(newTag);

            var links = CreateLink(newTag.Id);
            var linkedResource = resultResource.ToDynamic() as IDictionary<string, object>;
            linkedResource.Add("links", links);

            return CreatedAtRoute("GetTag", new { id = linkedResource["Id"] }, linkedResource);
        }

        /// <summary>
        /// 判断是存在
        /// </summary>
        [HttpPost("{id}", Name = "IsExistTag")]
        public async Task<IActionResult> IsExist(string key)
        {
            if (key.IsNullOrWhiteSpace()) return BadRequest();

            var article = await _tagRepository.GetListAsync(x => x.TagKey == key);
            if (article == null)
                return NoContent();

            return Forbid("此Key已存在！");
        }

        [HttpDelete("{id}", Name = "DeleteTag")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();

            var tag = await _tagRepository.GetAsync(id);
            if (null == tag) return NotFound();

            _tagRepository.Delete(tag);

            if (!await _unitOfWork.SaveAsync())
            {
                throw new Exception($"执行 {id} 删除时保存失败。");
            }
            return NoContent();
        }

        [HttpPut("{id}", Name = "UpdateTag")]
        //[RequestHeaderMatchingMediaType("Content-Type", new[] { "application/vnd.wyduang.article.update+json" })]
        public async Task<IActionResult> Update(int id, [FromBody] ArticleUpdateResource articleUpdateResource)
        {
            if (null == articleUpdateResource || id <= 0 ) return BadRequest();
            if (!ModelState.IsValid)
                return new UnprocessableEntityObjectResult(ModelState);

            var article = await _tagRepository.GetAsync(id);
            if (article == null) return NotFound();

            _mapper.Map(articleUpdateResource, article);

            if (!await _unitOfWork.SaveAsync())
            {
                throw new Exception($"更新标签 {id} 时保存失败。");
            }
            return NoContent();
        }

        private string CreateUri(TagParameter parameters, PaginationUriType uriType)
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
                        title = parameters.DisplayName
                    };
                    return _urlHelper.Link("GetTags", previousParameters);
                case PaginationUriType.NextPage:
                    var nextParameters = new
                    {
                        pageIndex = parameters.PageIndex + 1,
                        pageSize = parameters.PageSize,
                        orderBy = parameters.OrderBy,
                        fields = parameters.Fields,
                        title = parameters.DisplayName
                    };
                    return _urlHelper.Link("GetTags", nextParameters);
                default:
                    var currentParameters = new
                    {
                        pageIndex = parameters.PageIndex,
                        pageSize = parameters.PageSize,
                        orderBy = parameters.OrderBy,
                        fields = parameters.Fields,
                        title = parameters.DisplayName
                    };
                    return _urlHelper.Link("GetTags", currentParameters);
            }
        }

        private IEnumerable<LinkResource> CreateLink(int id, string fields = null)
        {
            var links = new List<LinkResource>();

            if (string.IsNullOrWhiteSpace(fields))
                links.Add(new LinkResource(_urlHelper.Link("GetTag", new { id }), "self", "GET"));
            else
                links.Add(new LinkResource(_urlHelper.Link("GetTag", new { id, fields }), "self", "GET"));
            links.Add(new LinkResource(_urlHelper.Link("DeleteTag", new { id }), "delete_tag", "DELETE"));

            return links;
        }

        private IEnumerable<LinkResource> CreateLinks(TagParameter parameter, bool hasPrevious, bool hasNext)
        {
            var links = new List<LinkResource>
            {
                new LinkResource(CreateUri(parameter, PaginationUriType.CurrentPage), "self", "GET")
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
