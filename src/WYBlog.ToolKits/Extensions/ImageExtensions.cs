using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WYBlog.Extensions
{
    public static class ImageExtensions
    {
        /// <summary>
        /// 添加水印，并保存处理好的图片
        /// </summary>
        /// <param name="imgPath">原图地址</param>
        /// <param name="watermarkImgPath">水印图地址</param>
        /// <returns></returns>
        public static async Task AddWatermarkAndSaveItAsync(this string imgPath, string watermarkImgPath = "")
        {
            //watermarkImgPath = watermarkImgPath.IsNullOrEmpty() ? Path.Combine(Directory.GetCurrentDirectory(), "Resources/watermark.png") : watermarkImgPath;

            var watermarkBytes = await File.ReadAllBytesAsync(watermarkImgPath);
            var imgBytes = await File.ReadAllBytesAsync(imgPath);

            var watermarkImg = Image.Load(watermarkBytes);
            var img = Image.Load(imgBytes, out IImageFormat format);

            watermarkImg.Mutate(context =>
            {
                context.DrawImage(img, 0.8F);
            });

            var newImgBase64 = watermarkImg.ToBase64String(format);

            var regex = new Regex("data:image/(.*);base64,");
            newImgBase64 = regex.Replace(newImgBase64, "");

            var bytes = Convert.FromBase64String(newImgBase64);

            await bytes.DownloadAsync(imgPath);
        }
    }
}