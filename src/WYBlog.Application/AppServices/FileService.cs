using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Validation;
using WYBlog.Configurations;
using WYBlog.Dtos;
using WYBlog.IAppServices;

namespace WYBlog.AppServices
{
    public class FileService : ApplicationService, IFileService
    {
        public async Task<string> CreateAsync(FileUploadInputDto input)
        {
            if (input.Bytes.IsNullOrEmpty())
            {
                throw new AbpValidationException("Bytes can not be null or empty!",
                    new List<ValidationResult>
                    {
                        new ValidationResult("Bytes can not be null or empty!", new[] {"Bytes"})
                    });
            }

            if (input.Bytes.Length > AppSettings.FileUpload.MaxFileSize)
            {
                throw new UserFriendlyException($"File exceeds the maximum upload size ({AppSettings.FileUpload.MaxFileSize / 1024 / 1024} MB)!");
            }

            if (!AppSettings.FileUpload.AllowedUploadFormats.Contains(Path.GetExtension(input.Name)))
            {
                throw new UserFriendlyException("Not a valid file format!");
            }

            var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(input.Name);
            var filePath = Path.Combine(AppSettings.FileUpload.FileUploadLocalFolder, fileName);

            if (!Directory.Exists(AppSettings.FileUpload.FileUploadLocalFolder))
            {
                Directory.CreateDirectory(AppSettings.FileUpload.FileUploadLocalFolder);
            }

            await File.WriteAllBytesAsync(filePath, input.Bytes);

            return await Task.FromResult("/api/file/" + fileName);
        }

        public async Task<byte[]> GetAsync(string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var filePath = Path.Combine(AppSettings.FileUpload.FileUploadLocalFolder, name);
            if (File.Exists(filePath))
            {
                return await File.ReadAllBytesAsync(filePath);
            }

            return await Task.FromResult(new byte[0]);
        }
    }
}