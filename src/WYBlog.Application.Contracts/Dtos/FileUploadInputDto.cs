using System;
using System.Collections.Generic;
using System.Text;

namespace WYBlog.Dtos
{
    public class FileUploadInputDto
    {
        public byte[] Bytes { get; set; }

        public string Name { get; set; }
    }
}
