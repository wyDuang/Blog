using AutoMapper;
using Blog.Core.BaseModels;
using Blog.Core.Entities;
using Blog.Core.Interfaces;
using Blog.Core.QueryParameters;
using Blog.Infrastructure.Extensions;
using Blog.Infrastructure.Filter;
using Blog.Infrastructure.Resources;
using Blog.Infrastructure.Resources.Hateoas;
using Blog.Infrastructure.ResultModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Site.Controllers.Api
{
    [Route("api/articles")]
    public class ArticleController : ApiBaseController
    {
        private readonly IArticleRepository _articleRepository;
        public ArticleController(
            IArticleRepository articleRepository,
            IUnitOfWork unitOfWork,
            ILogger<ArticleController> logger,
            IMapper mapper,
            IUrlHelper urlHelper,
            ITypeHelperService typeHelperService,
            IPropertyMappingContainer propertyMappingContainer)
            : base(unitOfWork, logger, mapper, urlHelper, typeHelperService, propertyMappingContainer)
        {
            _articleRepository = articleRepository;
        }

        /// <summary>
        /// 分页获取文章
        /// </summary>
        [HttpGet(Name = "GetArticles")]
        public async Task<IActionResult> Gets(ArticleParameter parameter, [FromHeader(Name = "Accept")] string mediaType)
        {
            if (!_propertyMappingContainer.ValidateMappingExistsFor<ArticleResource, Article>(parameter.OrderBy))
                return BadRequest("找不到要排序的字段。");

            if (!_typeHelperService.TypeHasProperties<ArticleResource>(parameter.Fields))
                return BadRequest("字段不存在。");

            var pagedList = await _articleRepository.GetPageListAsync(parameter, _propertyMappingContainer.Resolve<ArticleResource, Article>());
            var articleResources = _mapper.Map<IEnumerable<Article>, IEnumerable<ArticleResource>>(pagedList);

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

                var links = CreateLinksForArticles(parameter, pagedList.HasPrevious, pagedList.HasNext);

                var shapedResources = articleResources.ToDynamicIEnumerable(parameter.Fields);
                var shapedWithLinks = shapedResources.Select(x =>
                {
                    var dict = x as IDictionary<string, object>;
                    var links = CreateLinksForArticle((int)dict["Id"], parameter.Fields);
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
                var previousPageLink = pagedList.HasPrevious ? CreateArticleUri(parameter, PaginationUriType.PreviousPage) : null;
                var nextPageLink = pagedList.HasNext ? CreateArticleUri(parameter, PaginationUriType.NextPage) : null;

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

                return Ok(articleResources.ToDynamicIEnumerable(parameter.Fields));
            }
        }

        /// <summary> 
        /// 根据ID获取一篇文章
        /// </summary>
        //[DisableCors]
        [HttpGet("{id}", Name = "GetArticle")]
        public async Task<IActionResult> Get(int id, string fields = null)
        {
            if (!_typeHelperService.TypeHasProperties<ArticleResource>(fields))
                return BadRequest("字段不存在。");

            var article = await _articleRepository.GetAsync(id);
            if (article == null) 
                return NotFound();

            var articleResource = _mapper.Map<Article, ArticleResource>(article);

            var links = CreateLinksForArticle(id, fields);
            var result = articleResource.ToDynamic(fields) as IDictionary<string, object>;
            result.Add("links", links);

            return Ok(result);
        }

        /// <summary>
        /// 创建文章
        /// </summary>
        [HttpPost(Name = "CreateArticle")]
        //[RequestHeaderMatchingMediaType("Content-Type", new[] { "application/vnd.wyduang.article.create+json" })]
        //[RequestHeaderMatchingMediaType("Accept", new[] { "application/vnd.wyduang.article.display+json" })]
        public async Task<IActionResult> Post([FromBody] ArticleAddResource articleAddResource)
        {
            if (articleAddResource == null)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                return new MyUnprocessableEntityObjectResult(ModelState);
            }

            var newArticle = _mapper.Map<ArticleAddResource, Article>(articleAddResource);

            newArticle.Author = "wyDuang";
            newArticle.CreateDate = DateTime.Now;
            _articleRepository.Add(newArticle);

            if (!await _unitOfWork.SaveAsync())
                throw new Exception("保存失败！");

            var resultResource = _mapper.Map<Article, ArticleResource>(newArticle);

            var links = CreateLinksForArticle(newArticle.Id); 
            var linkedArticleResource = resultResource.ToDynamic() as IDictionary<string, object>;
            linkedArticleResource.Add("links", links);

            return CreatedAtRoute("GetArticle", new { id = linkedArticleResource["Id"] }, linkedArticleResource);
        }

        /// <summary>
        /// 判断是存在
        /// </summary>
        [HttpPost("{id}", Name = "BlockCreatingArticle")]
        public async Task<IActionResult> BlockCreating(int id)
        {
            var article = await _articleRepository.GetAsync(id);
            if (article == null) return NotFound();
            
            return StatusCode(StatusCodes.Status409Conflict);
        }

        [HttpDelete("{id}", Name = "DeleteArticle")]
        public async Task<IActionResult> Delete(int id)
        {
            var article = await _articleRepository.GetAsync(id);
            if (article == null) return NotFound();

            _articleRepository.Delete(article);

            if (!await _unitOfWork.SaveAsync())
            {
                throw new Exception($"执行 {id} 删除时保存失败。");
            }
            return NoContent();
        }

        [HttpPut("{id}", Name = "UpdateArticle")]
        //[RequestHeaderMatchingMediaType("Content-Type", new[] { "application/vnd.wyduang.article.update+json" })]
        public async Task<IActionResult> Update(int id, [FromBody] ArticleUpdateResource articleUpdateResource)
        {
            if (articleUpdateResource == null) return BadRequest();
            if (!ModelState.IsValid)
                return new UnprocessableEntityObjectResult(ModelState);

            var article = await _articleRepository.GetAsync(id);
            if (article == null) return NotFound();

            _mapper.Map(articleUpdateResource, article);

            if (!await _unitOfWork.SaveAsync())
            {
                throw new Exception($"Updating product {id} failed when saving.");
            }
            return NoContent();
        }

        private string CreateArticleUri(ArticleParameter parameters, PaginationUriType uriType)
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
                        title = parameters.Title
                    };
                    return _urlHelper.Link("GetArticles", previousParameters);
                case PaginationUriType.NextPage:
                    var nextParameters = new
                    {
                        pageIndex = parameters.PageIndex + 1,
                        pageSize = parameters.PageSize,
                        orderBy = parameters.OrderBy,
                        fields = parameters.Fields,
                        title = parameters.Title
                    };
                    return _urlHelper.Link("GetArticles", nextParameters);
                default:
                    var currentParameters = new
                    {
                        pageIndex = parameters.PageIndex,
                        pageSize = parameters.PageSize,
                        orderBy = parameters.OrderBy,
                        fields = parameters.Fields,
                        title = parameters.Title
                    };
                    return _urlHelper.Link("GetArticles", currentParameters);
            }
        }

        private IEnumerable<LinkResource> CreateLinksForArticle(int id, string fields = null)
        {
            var links = new List<LinkResource>();

            if (string.IsNullOrWhiteSpace(fields))
                links.Add(new LinkResource(_urlHelper.Link("GetArticle", new { id }), "self", "GET"));
            else
                links.Add(new LinkResource(_urlHelper.Link("GetArticle", new { id, fields }), "self", "GET"));
            links.Add(new LinkResource(_urlHelper.Link("DeleteArticle", new { id }), "delete_post", "DELETE"));

            return links;
        }

        private IEnumerable<LinkResource> CreateLinksForArticles(ArticleParameter parameter, bool hasPrevious, bool hasNext)
        {
            var links = new List<LinkResource>
            {
                new LinkResource( CreateArticleUri(parameter, PaginationUriType.CurrentPage), "self", "GET")
            };

            if (hasPrevious)
            {
                links.Add(new LinkResource(CreateArticleUri(parameter, PaginationUriType.PreviousPage), "previous_page", "GET"));
            }
            if (hasNext)
            {
                links.Add(new LinkResource(CreateArticleUri(parameter, PaginationUriType.NextPage), "next_page", "GET"));
            }

            return links;
        }
    }
}
