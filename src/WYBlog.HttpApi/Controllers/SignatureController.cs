using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WYBlog.Controllers
{
    //[ApiExplorerSettings(GroupName = ApiGroupingConsts.GroupName_v2)]
    public class SignatureController : BaseController
    {
        /// <summary>
        /// 获取签名
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok();
        }
    }
}