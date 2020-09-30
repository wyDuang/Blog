using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WYBlog.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class UploadController: BaseController
    {
        private readonly IWebHostEnvironment _env;
        public UploadController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpPost("upload")]
        public IActionResult UploadFile(List<IFormFile> files)
        {
            if (files.Count <= 0)
            {
                //result.Message = "上传文件不能为空";
                //return result;
            }

            #region 上传

            List<string> filenames = new List<string>();

            var webRootPath = _env.WebRootPath;

            var physicalPath = $"{webRootPath}/";

            if (!Directory.Exists(physicalPath))
            {
                Directory.CreateDirectory(physicalPath);
            }

            foreach (var file in files)
            {
                var fileExtension = Path.GetExtension(file.FileName);//获取文件格式，拓展名

                var saveName = $"Files/{Path.GetRandomFileName()}{fileExtension}";
                filenames.Add(saveName);//相对路径

                var fileName = webRootPath + saveName;

                using FileStream fs = System.IO.File.Create(fileName);
                file.CopyTo(fs);
                fs.Flush();
            }
            #endregion

            return Ok();
        }
    }
}
