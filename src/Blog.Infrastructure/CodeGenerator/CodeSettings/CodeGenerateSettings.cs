using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.CodeGenerator.CodeSettings
{
    /// <summary>
    /// 代码生成选项
    /// </summary>
    public class CodeGenerateSettings
    {
        /// <summary>
        /// 代码生成时间
        /// </summary>
        public string GeneratorTime { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        /// <summary>
        /// 输出路径
        /// </summary>
        public string OutputPath { get; set; }
        /// <summary>
        /// 实体命名空间
        /// </summary>
        public string ModelsNamespace { get; set; }
        /// <summary>
        /// 资源实体命名空间
        /// </summary>
        public string ResourcesNamespace { get; set; }
        /// <summary>
        /// 仓储接口命名空间
        /// </summary>
        public string IRepositoryNamespace { get; set; }
        /// <summary>
        /// 仓储命名空间
        /// </summary>
        public string RepositoryNamespace { get; set; }
        /// <summary>
        /// 服务接口命名空间
        /// </summary>
        public string IServicesNamespace { get; set; }
        /// <summary>
        /// 服务命名空间
        /// </summary>
        public string ServicesNamespace { get; set; }
        /// <summary>
        /// DbContext命名空间
        /// </summary>
        public string DbContextNamespace { get; set; }
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// 数据库类型
        /// </summary>
        public string DbType { get; set; }
    }
}
