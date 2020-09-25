using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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