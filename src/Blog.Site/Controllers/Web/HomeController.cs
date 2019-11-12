using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Core.Interfaces;
using Blog.Core.QueryParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Blog.Site.Controllers.Web
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly ITagRepository _tagRepository;
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public HomeController(
            ICategoryRepository categoryRepository,
            IArticleRepository articleRepository,
            ITagRepository tagRepository,
            ILogger<HomeController> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _tagRepository = tagRepository;
            _categoryRepository = categoryRepository;
            _articleRepository = articleRepository;
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [Route("/")]
        public async Task<IActionResult> Index(ArticleParameter articleParameter)
        {
            var pageList = await _articleRepository.GetPageListAsync(articleParameter, null);
            return View(pageList);
        }

        [Route("/a/{articleKey}.html")]
        public IActionResult Details(string articleKey)
        {
            return View();
        }

        [Route("/about.html")]
        public IActionResult About()
        {
            return View();
        }
    }
}