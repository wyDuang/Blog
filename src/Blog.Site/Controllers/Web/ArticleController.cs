﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Site.Controllers.Web
{
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ArticleController : Controller
    {
        [Route("/articles.html")]
        public IActionResult Index()
        {
            return View();
        }
    }
}