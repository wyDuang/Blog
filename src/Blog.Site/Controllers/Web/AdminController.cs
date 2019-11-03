using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Core.Entities;
using Blog.Core.Interfaces;
using Blog.Infrastructure.Resources;
using Blog.Infrastructure.ResultModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Blog.Site.Controllers.Web
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class AdminController : Controller
    {
        private readonly ITagRepository _tagRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly ILogger<AdminController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AdminController(
            ITagRepository tagRepository,
            ICategoryRepository categoryRepository,
            IArticleRepository articleRepository,
            IUnitOfWork unitOfWork,
            ILogger<AdminController> logger,
            IMapper mapper)
        {
            _tagRepository = tagRepository;
            _categoryRepository = categoryRepository;
            _articleRepository = articleRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet, Route("/admin/add")]
        public async Task<IActionResult> AddOrUpdateArticle(int id = 0)
        {
            var model = new Article();
            if(id > 0)
            {
                model = await _articleRepository.GetAsync(id);
            }
            var categories = await _categoryRepository.GetAllListAsync();
            ViewBag.Categories = categories;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveArticle([FromForm] ArticleAddOrUpdateResource articleResource) 
        {
            if (articleResource == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return new MyUnprocessableEntityObjectResult(ModelState);
            }

            var newArticle = _mapper.Map<ArticleAddOrUpdateResource, Article>(articleResource);
            newArticle.Author = "wyDuang";
            newArticle.CreateDate = DateTime.Now;

            _articleRepository.Add(newArticle);
            if (!await _unitOfWork.SaveAsync())
            {
                throw new Exception("Save Failed!");
            }

            return RedirectToAction("AddOrUpdateArticle");
        }

        [Route("/login")]
        public IActionResult Login()
        {
            return View();
        }
    }
}
