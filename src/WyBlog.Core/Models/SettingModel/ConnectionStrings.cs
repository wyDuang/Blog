using System;
using System.Collections.Generic;
using System.Text;

namespace WyBlog.Core.Models
{
    public class ConnectionStrings
    {
        /// <summary>
        /// MySql数据库链接
        /// </summary>
        public string MySqlConnection { get; set; }
        /// <summary>
        /// SqlServer数据库链接
        /// </summary>
        public string SqlServerConnection { get; set; }
    }
}
