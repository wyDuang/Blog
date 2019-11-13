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
    [Route("api/friendLinks")]
    [ApiExplorerSettings(GroupName = ApiVersionConsts.GroupName_v1)]
    public class FriendLinkController: BaseController
    {
        public FriendLinkController()
        {

        }

        private readonly IFriendLinkRepository _tagRepository;
        public FriendLinkController(
            IFriendLinkRepository tagRepository,
            IUnitOfWork unitOfWork,
            ILogger<FriendLinkController> logger,
            IMapper mapper,
            IUrlHelper urlHelper,
            ITypeHelperService typeHelperService,
            IPropertyMappingContainer propertyMappingContainer)
            : base(unitOfWork, logger, mapper, urlHelper, typeHelperService, propertyMappingContainer)
        {
            _tagRepository = tagRepository;
        }
    }
}
