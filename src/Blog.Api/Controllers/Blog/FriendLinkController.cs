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
    [Route("friendLinks")]
    [ApiExplorerSettings(GroupName = ApiVersionConsts.GroupName_v1)]
    public class FriendLinkController: BaseController
    {
        private readonly IFriendLinkRepository _friendLinkRepository;
        public FriendLinkController(
            IUnitOfWork unitOfWork,
            ILogger<FriendLinkController> logger,
            IMapper mapper,
            IUrlHelper urlHelper,
            ITypeHelperService typeHelperService,
            IPropertyMappingContainer propertyMappingContainer,
            IFriendLinkRepository friendLinkRepository)
            : base(unitOfWork, logger, mapper, urlHelper, typeHelperService, propertyMappingContainer)
        {
            _friendLinkRepository = friendLinkRepository;
        }

        /// <summary>
        /// 获取所有的友情链接
        /// </summary>
        //[AllowAnonymous]
        [HttpGet(Name = "GetFriendLinks")]
        public async Task<IActionResult> GetFriendLinks()
        {
            var allList = await _friendLinkRepository.GetListAsync(x => x.IsDeleted == 0);
                
            var friendLinkResources = _mapper.Map<IEnumerable<FriendLink>, IEnumerable<FriendLinkResource>>(allList);

            return Ok(friendLinkResources);
        }



    }
}
