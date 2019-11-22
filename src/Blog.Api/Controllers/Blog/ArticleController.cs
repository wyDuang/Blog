using AutoMapper;
using Blog.Core.BaseModels;
using Blog.Core.Entities;
using Blog.Core.Interfaces;
using Blog.Core.QueryParameters;
using Blog.Infrastructure.Extensions;
using Blog.Infrastructure.Attribute;
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
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Blog.Api.Controllers
{
    [Authorize]
    [Route("articles")]
    [ApiExplorerSettings(GroupName = ApiVersionConsts.GroupName_v1)]
    public class ArticleController : BaseController
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
        /// <param name="parameter">查询参数</param>
        /// <param name="mediaType"></param>
        /// <returns>返回文章List</returns>
        /// <response code="200">返回文章List</response>
        /// <response code="400">找不到要排序的字段/查询指定字段</response>  
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpGet(Name = "GetArticles")]
        public async Task<IActionResult> GetArticles(ArticleParameter parameter, [FromHeader(Name = "Accept")] string mediaType)
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

                var linkedResult = new
                {
                    value = shapedWithLinks,
                    links
                };
                return Ok(linkedResult);
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
        public async Task<IActionResult> GetArticle(int id, string fields = null)
        {
            if (!_typeHelperService.TypeHasProperties<ArticleResource>(fields))
                return BadRequest("字段不存在。");

            if (id <= 0) return BadRequest();

            var article = await _articleRepository.GetAsync(id);
            if (null == article)
                return NotFound();

            var articleResource = _mapper.Map<Article, ArticleResource>(article);

            var links = CreateLinksForArticle(id, fields);
            var result = articleResource.ToDynamic(fields) as IDictionary<string, object>;
            result.Add("links", links);

            return Ok(result);
        }

        [AllowAnonymous]
        /// <summary>
        /// 创建文章
        /// </summary>
        [HttpPost(Name = "CreateArticle")]
        //[RequestHeaderMatchingMediaType("Content-Type", new[] { "application/vnd.wyduang.article.create+json" })]
        //[RequestHeaderMatchingMediaType("Accept", new[] { "application/vnd.wyduang.article.display+json" })]
        public async Task<IActionResult> CreateArticle(ArticleAddResource articleAddResource)
        {
            if (null == articleAddResource) return BadRequest();

            if (!ModelState.IsValid)
                return new MyUnprocessableEntityObjectResult(ModelState);

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
        /// 根据key判断是否存在
        /// </summary>
        [HttpPost("{id}", Name = "IsExistArticle")]
        public async Task<IActionResult> IsExistArticle(string key)
        {
            if (key.IsNullOrWhiteSpace()) return BadRequest();

            var article = await _articleRepository.GetListAsync(x => x.ArticleKey == key);
            if (article == null) return NotFound();//404资源不存在

            return StatusCode(StatusCodes.Status409Conflict);//此Key已存在！
        }

        [HttpPut("{id}", Name = "UpdateArticle")]
        //[RequestHeaderMatchingMediaType("Content-Type", new[] { "application/vnd.wyduang.article.update+json" })]
        public async Task<IActionResult> UpdateArticle(int id, [FromBody] ArticleUpdateResource articleUpdateResource)
        {
            if (null == articleUpdateResource || id <= 0) return BadRequest();

            if (!ModelState.IsValid)
                return new UnprocessableEntityObjectResult(ModelState);

            var article = await _articleRepository.GetAsync(id);
            if (null == article) return NotFound();

            _mapper.Map(articleUpdateResource, article);

            if (!await _unitOfWork.SaveAsync())
            {
                throw new Exception($"更新文章 {id} 时保存失败。");
            }
            return NoContent();
        }

        /// <summary>
        /// 根据Id删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}", Name = "DeleteArticle")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            if (id <= 0) return BadRequest();

            var article = await _articleRepository.GetAsync(id);
            if (null == article) return NotFound();

            _articleRepository.Delete(article);

            if (!await _unitOfWork.SaveAsync())
            {
                throw new Exception($"执行 {id} 删除时保存失败。");
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
