using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Core.Entities;
using Blog.Core.Interfaces;
using Blog.Core.QueryParameters;
using Blog.Infrastructure.CodeGenerator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Blog.Site.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        //private readonly CodeGenerator CodeGenerator;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly ITagRepository _tagRepository;
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public HomeController(
            //CodeGenerator codeGenerator,
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
            //CodeGenerator = codeGenerator;
        }

        [Route("/")]
        public async Task<IActionResult> Index(ArticleParameter articleParameter)
        {
            //CodeGenerator.GenerateTemplateCodesFromDatabase(true);

            var pageList = await _articleRepository.GetPageListAsync(articleParameter, null);
            return View(pageList);
        }

        [Route("/{categoryKey}/{articleKey}.html")]
        public IActionResult Article(string categoryKey, string articleKey)
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