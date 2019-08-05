using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WyBlog.Core.Helper;
using WyBlog.Core.Models;
using WyBlog.Entities;

namespace WyBlog.Web.Controllers.Api
{
    [Route("api/signature")]
    [ApiController]
    [Authorize]
    public class SignatureController : ControllerBase
    {
        private AppSettings _appSettings;
        private readonly IHostingEnvironment _env;
        private readonly IHttpClientFactory _clientFactory;
        public SignatureController(
            IOptionsSnapshot<AppSettings> appSettings,
            IHostingEnvironment env,
            IHttpClientFactory clientFactory)
        {
            _appSettings = appSettings.Value;
            _env = env;
            _clientFactory = clientFactory;
        }

        /// <summary>
        /// 艺术签名
        /// </summary>
        /// <param name="signatureEntity">参数对象</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<DataResult> Get(SignatureEntity signatureEntity)
        {
            return await GetSignature(signatureEntity.Name, (SignatureEnum)signatureEntity.Type);

            //return new string[] { "value1", "value2" };
        }

        [NonAction]
        public async Task<DataResult> GetSignature(string name, SignatureEnum signature)
        {
            DataResult result = new DataResult();
            const string urlPath = "http://www.jiqie.com/a/re22.php";
            string urlContent = $"id={name}&idi=jiqie&id1=800&id2={(int)signature}&id3=#000000&id4=#000000&id5=#000000&id6=#000000";

            try
            {
                var client = _clientFactory.CreateClient();
                var fromUrlContent = new StringContent(urlContent);
                fromUrlContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

                var response = await client.PostAsync(urlPath, fromUrlContent);
                if (response.IsSuccessStatusCode)
                {
                    var htmlContent = await response.Content.ReadAsStringAsync();
                    var signatureUrl = htmlContent.Replace("<img src=\"", "").Replace("\">", "");

                    var signaturePath = $"{_env.WebRootPath}/files/signature/{name}.jpg";
                    FileHelper.DownLoad(signatureUrl, signaturePath);//500*200像素

                    var watermarkPath = _env.WebRootPath + _appSettings.WatermarkPath;
                    var signatureImgBytes = await System.IO.File.ReadAllBytesAsync(signaturePath);
                    var watermarkImgBytes = await System.IO.File.ReadAllBytesAsync(watermarkPath);

                    var signatureImg = Image.Load(signatureImgBytes, out IImageFormat format);
                    var watermarkImg = Image.Load(watermarkImgBytes);

                    watermarkImg.Mutate(o => o.Resize(watermarkImg.Width / 4, watermarkImg.Height / 4));//缩小倍数
                    signatureImg.Mutate(o => {

                        o.DrawImage(watermarkImg, new Point(450, 150), 1);
                    });

                    var imgBase64 = signatureImg.ToBase64String(format);
                    var reg = new Regex("data:image/(.*);base64,");
                    imgBase64 = reg.Replace(imgBase64, "");
                    var bytes = Convert.FromBase64String(imgBase64);

                    signatureUrl = $"/files/signature/{name}{signature}.jpg";
                    FileHelper.SaveFile(bytes, _env.WebRootPath + signatureUrl);
                    FileHelper.Delete(signaturePath);

                    var entity = new SignatureEntity
                    {
                        Name = name,
                        Type = (int)signature,
                        Url = signatureUrl
                    };
                    result.Data = entity;
                }
                else
                {
                    result.Code = 500;
                    result.Msg = "获取签名异常，请与1014558384@qq.com联系。";
                }
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Msg = ex.Message;
            }
            return result;
        }
    }
}
