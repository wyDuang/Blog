using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WyBlog.Web.Controllers.Web
{
    public class ArticleController : Controller
    {
        [Route("/articles.html")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
