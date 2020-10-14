using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using WYBlog.Dtos;

namespace WYBlog.IAppServices
{
    public interface IFileService : IApplicationService
    {
        Task<byte[]> GetAsync(string name);

        Task<string> CreateAsync(FileUploadInputDto input);
    }
}
