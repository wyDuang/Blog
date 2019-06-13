using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Site.Controllers.Web
{
    public class HomeController : Controller
    {
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
        public IActionResult Index()
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