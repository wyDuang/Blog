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
    [ApiExplorerSettings(IgnoreApi = true)]//应忽略控制器或动作
    public class HomeController : Controller
    {
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
        [Route("/")]
<<<<<<< HEAD
        [Route("/index.html")]
=======
>>>>>>> e7f3251... 继续
=======
        [Route("/")]
        [Route("/index.html")]
>>>>>>> 1ff30bd... 增加blog首页和文章页
=======
>>>>>>> 9197bf0... 集成layui+editor.md编辑器
=======
        [Route("/index.html")]
>>>>>>> 2ab0d7d... 修改路由
        public IActionResult Index()
=======
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
        }

        [Route("/")]
        public async Task<IActionResult> Index(ArticleParameter articleParameter)
        {
            var pageList = await _articleRepository.GetPageListAsync(articleParameter, null);
            return View(pageList);
        }

        [Route("/a/{articleKey}.html")]
        public IActionResult Details(string articleKey)
>>>>>>> 44c5959... 添加api
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