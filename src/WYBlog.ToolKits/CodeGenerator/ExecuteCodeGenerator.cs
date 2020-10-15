using Microsoft.Extensions.Options;
using System;

namespace WYBlog.CodeGenerator
{
    /// <summary>
    /// 代码生成器。参考自：Zxw.Framework.NetCore
    /// <remarks>
    /// 根据数据库表以及表对应的列生成对应的数据库实体
    /// </remarks>
    /// </summary>
    public class ExecuteCodeGenerator
    {
        private readonly string Delimiter = "\\";//分隔符，默认为windows下的\\分隔符

        private readonly CodeGenerateConfig codeGenerateOptions;

        public ExecuteCodeGenerator(IOptionsSnapshot<CodeGenerateConfig> options)
        {
            if (null == options)
            {
                throw new ArgumentNullException(nameof(options));
            }
            codeGenerateOptions = options.Value;

            if (codeGenerateOptions.ConnectionString.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException("不指定【数据库连接串】就生成代码，你想上天吗？");
            }

            if (codeGenerateOptions.DbType.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException("不指定【数据库类型】就生成代码，你想逆天吗？");
            }

            var path = AppDomain.CurrentDomain.BaseDirectory;
            if (codeGenerateOptions.OutputPath.IsNullOrWhiteSpace())
            {
                codeGenerateOptions.OutputPath = path;
            }

            var flag = path.IndexOf("/bin");
            if (flag > 0)
            {
                Delimiter = "/";//如果可以取到值，修改分割符
            }
        }
    }
}