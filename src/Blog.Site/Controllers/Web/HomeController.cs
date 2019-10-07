using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Site.Controllers.Web
{
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : Controller
    {
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