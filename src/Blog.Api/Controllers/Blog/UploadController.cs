using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Core.Interfaces;
using Blog.Infrastructure.Swagger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Blog.Api.Controllers
{
    /// <summary>
    /// 文件上传
    /// </summary>
    [Authorize]
    [Route("uploads")]
    [ApiExplorerSettings(GroupName = ApiVersionConsts.GroupName_v1)]
    public class UploadController : BaseController
    {
        public UploadController(
            IUnitOfWork unitOfWork,
            ILogger<ArticleController> logger,
            IMapper mapper,
            IUrlHelper urlHelper,
            ITypeHelperService typeHelperService,
            IPropertyMappingContainer propertyMappingContainer)
            : base(unitOfWork, logger, mapper, urlHelper, typeHelperService, propertyMappingContainer)
        {

        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="formFile">文件</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Tests(IFormFile formFile)
        {
            return Ok("靓仔");
        }
    }
}
