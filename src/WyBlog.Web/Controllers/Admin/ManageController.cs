using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WyBlog.Web.Controllers.Admin
{
    public class ManageController : Controller
    {
        /// <summary>
        /// 后台管理首页
        /// </summary>
        /// <returns></returns>
        [Route("/admin")]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 文章列表
        /// </summary>
        /// <returns></returns>
        [Route("/admin/articles")]
        [Route("/admin/articles/page-{page:int:min(1)}")]
        public IActionResult Articles()
        {
            return View();
        }

        /// <summary>
        /// 文章添加
        /// </summary>
        /// <returns></returns>
        [Route("/admin/add_article")]
        public IActionResult AddArticle()
        {
            return View();
        }

        /// <summary>
        /// 文章编辑
        /// </summary>
        /// <returns></returns>
        [Route("/admin/edit_article/{id:int:min(1)}")]
        public IActionResult EditArticle()
        {
            return View();
        }

        /// <summary>
        /// 分类管理
        /// </summary>
        /// <returns></returns>
        [Route("/admin/categories")]
        public IActionResult Categories()
        {
            return View();
        }

        /// <summary>
        /// 标签管理
        /// </summary>
        /// <returns></returns>
        [Route("/admin/tags")]
        public IActionResult Tags()
        {
            return View();
        }

        /// <summary>
        /// 标签列表（添加和编辑文章中需要）
        /// </summary>
        /// <returns></returns>
        [Route("/admin/select_tags")]
        public IActionResult SelectTags()
        {
            return View();
        }
    }
}
