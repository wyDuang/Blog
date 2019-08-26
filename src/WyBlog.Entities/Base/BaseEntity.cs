using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WyBlog.Core.Models;

namespace WyBlog.Entities
{
    public abstract class BaseEntity : IBaseEntity
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public int Id { get; set; }
    }
}
