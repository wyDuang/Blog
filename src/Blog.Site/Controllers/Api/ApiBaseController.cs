using AutoMapper;
using Blog.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Site.Controllers.Api
{
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly ILogger<ControllerBase> _logger;
        protected readonly IMapper _mapper;
        protected readonly IUrlHelper _urlHelper;
        protected readonly ITypeHelperService _typeHelperService;
        protected readonly IPropertyMappingContainer _propertyMappingContainer;
        public ApiBaseController(
            IUnitOfWork unitOfWork = null,
            ILogger<ControllerBase> logger = null,
            IMapper mapper = null,
            IUrlHelper urlHelper = null,
            ITypeHelperService typeHelperService = null,
            IPropertyMappingContainer propertyMappingContainer = null)
        {
            if (null != unitOfWork) _unitOfWork = unitOfWork;
            if (null != logger) _logger = logger;
            if (null != mapper) _mapper = mapper;
            if (null != urlHelper) _urlHelper = urlHelper;
            if (null != typeHelperService) _typeHelperService = typeHelperService;
            if (null != propertyMappingContainer) _propertyMappingContainer = propertyMappingContainer;
        }
    }
}
