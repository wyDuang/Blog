using AutoMapper;
using Blog.Core.Interfaces;
using Blog.Infrastructure.Swagger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Blog.Api.Controllers
{
    [Route("common")]
    [ApiExplorerSettings(GroupName = ApiVersionConsts.GroupName_v2)]
    public class CommonController: BaseController
    {
        private readonly IHttpClientFactory _httpClient;
        public CommonController(IHttpClientFactory httpClient,
            IUnitOfWork unitOfWork,
            ILogger<CommonController> logger,
            IMapper mapper,
            IUrlHelper urlHelper,
            ITypeHelperService typeHelperService,
            IPropertyMappingContainer propertyMappingContainer)
            : base(unitOfWork, logger, mapper, urlHelper, typeHelperService, propertyMappingContainer)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        [Route("bing")]
        public async Task<IActionResult> Get_Bing()
        {
            var api = "https://cn.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1&pid=hp&FORM=BEHPTB";
            using var httpClient = _httpClient.CreateClient();
            var json = await httpClient.GetStringAsync(api);
            var obj = JsonSerializer.Deserialize<dynamic>(json);
            var url = "https://cn.bing.com" + obj["images"][0]["url"].ToString();
            var bytes = await httpClient.GetByteArrayAsync(url);

            return File(bytes, "image/jpeg");
        }
    }
}
