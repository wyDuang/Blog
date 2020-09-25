using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

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
