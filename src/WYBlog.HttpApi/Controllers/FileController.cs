using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Http;
using WYBlog.Dtos;
using WYBlog.IAppServices;

namespace WYBlog.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class FileController : BaseController
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        /// <summary>
        /// 根据文件名得到文件
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{name:required}")]
        public async Task<FileResult> GetAsync(string name)
        {
            var bytes = await _fileService.GetAsync(name);
            return File(bytes, MimeTypes.GetByExtension(Path.GetExtension(name)));
        }

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("upload")]
        public async Task<JsonResult> UploadFile(IFormFile file)
        {
            if (file == null)
            {
                throw new UserFriendlyException("No file found!");
            }

            var bytes = await file.GetAllBytesAsync();
            var result = await _fileService.CreateAsync(new FileUploadInputDto()
            {
                Bytes = bytes,
                Name = file.FileName
            });

            return Json(result);
        }
    }
}