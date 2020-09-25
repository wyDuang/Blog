using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace WYBlog.Controllers
{
    [AllowAnonymous]
    //[ApiExplorerSettings(IgnoreApi = false)]//true表示不显示到swaggerUI
    public class AuthController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok();
        }
    }
}
