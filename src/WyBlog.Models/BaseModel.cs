using System;
using System.Collections.Generic;
using System.Text;

namespace WyBlog.Models
{
    public abstract class BaseModel
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        public int CreateUserId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; } = DateTime.Now;
        /// <summary>
        /// 修改人ID
        /// </summary>
        public int UpdateUserId { get; set; } 
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateDate { get; set; } = DateTime.Now;
    }
}
