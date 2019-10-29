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
        [Route("/index.html")]
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