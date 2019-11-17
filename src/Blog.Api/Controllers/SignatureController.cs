using Blog.Core.Entities;
using Blog.Core.SettingModels;
using Blog.Infrastructure.Helpers;
using Blog.Infrastructure.Resources;
using Blog.Infrastructure.ResultModel;
using Blog.Infrastructure.Swagger;
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
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Blog.Api.Controllers
{
    [Authorize]
    [Route("signature")]
    [ApiExplorerSettings(GroupName = ApiVersionConsts.GroupName_v2)]
    public class SignatureController : BaseController
    {
        private AppSettings _appSettings;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SignatureController(
            IOptionsSnapshot<AppSettings> appSettings,
            IHttpClientFactory clientFactory,
            IWebHostEnvironment webHostEnvironment)
        {
            _appSettings = appSettings.Value;            
            _clientFactory = clientFactory;
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// 艺术签名
        /// </summary>
        /// <param name="signatureEntity">参数对象</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<DataResult> Get(SignatureResource signatureEntity)
        {
            return await GetSignature(signatureEntity.Name, (SignatureEnum)signatureEntity.Type);

            //return new string[] { "value1", "value2" };
        }

        private async Task<DataResult> GetSignature(string name, SignatureEnum signature)
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

                    var signaturePath = $"/files/signature/{name}.jpg";
                    FileHelper.DownLoad(signatureUrl, signaturePath);//500*200像素

                    var watermarkPath = _webHostEnvironment + _appSettings.WatermarkPath;
                    var watermarkBytes = await System.IO.File.ReadAllBytesAsync(watermarkPath);
                    var watermarkImg = Image.Load(watermarkBytes, out IImageFormat format);
                    watermarkImg.Mutate(o => o.Resize(watermarkImg.Width / 4, watermarkImg.Height / 4));//缩小倍数

                    var signatureBytes = await System.IO.File.ReadAllBytesAsync(signaturePath);
                    var signatureImg = Image.Load(signatureBytes);
                    signatureImg.Mutate(context => {
                        context.DrawImage(watermarkImg, new Point(450, 150), 1);
                    });

                    var signatureImgBase64 = signatureImg.ToBase64String(format);
                    var reg = new Regex("data:image/(.*);base64,");
                    signatureImgBase64 = reg.Replace(signatureImgBase64, "");
                    var bytes = Convert.FromBase64String(signatureImgBase64);

                    signatureUrl = $"/files/signature/{name}{signature}.jpg";
                    FileHelper.SaveFile(bytes, _webHostEnvironment + signatureUrl);
                    FileHelper.Delete(signaturePath);

                    var entity = new SignatureResource
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

        private void AddWatermarkSaveItAsync(string signaturePath)
        {
            if (string.IsNullOrWhiteSpace(_webHostEnvironment.WebRootPath))
            {
                _webHostEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }

            //var watermarkPath = _webHostEnvironment + _appSettings.WatermarkPath;
            //var watermarkBytes = await System.IO.File.ReadAllBytesAsync(watermarkPath);
            //var signatureBytes = await System.IO.File.ReadAllBytesAsync(signaturePath);

            //var watermarkImg = Image.Load(watermarkBytes);
            //var signatureImg = Image.Load(signatureBytes, out IImageFormat format);

            //watermarkImg.Mutate(o => o.Resize(watermarkImg.Width / 4, watermarkImg.Height / 4));//缩小倍数
            //signatureImg.Mutate(o => {
            //    o.DrawImage(watermarkImg, new Point(450, 150), 1);
            //});


            //watermarkImg.Mutate(context =>
            //{
            //    context.DrawImage(signatureImg, 1);
            //});

        }
    }
}
