using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace WYBlog.Controllers
{
    [AllowAnonymous]
    [ApiExplorerSettings(GroupName = ApiGroupingConsts.GroupName_v2)]
    public class CommonController : BaseController
    {
        private readonly IHttpClientFactory _httpClient;
        public CommonController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// 获取必应图片
        /// </summary>
        /// <returns></returns>
        [HttpGet("bing")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetBingAsync()
        {
            var api = "https://cn.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1&pid=hp&FORM=BEHPTB";
            using var httpClient = _httpClient.CreateClient();
            var json = await httpClient.GetStringAsync(api);
            var obj = JsonConvert.DeserializeObject<dynamic>(json);
            var url = "https://cn.bing.com" + obj["images"][0]["url"].ToString();
            var bytes = await httpClient.GetByteArrayAsync(url);

            return File(bytes, "image/jpeg");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        [HttpGet("ip")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetIp2ReginAsync([FromQuery]string ip)
        {
            return Ok(ip);
        }
    }
}