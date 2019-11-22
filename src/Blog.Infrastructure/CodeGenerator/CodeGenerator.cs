using Blog.Infrastructure.CodeGenerator.CodeGenerateModels;
using Blog.Infrastructure.CodeGenerator.CodeSettings;
using Blog.Infrastructure.Extensions;
using Blog.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Blog.Infrastructure.CodeGenerator
{
    /// <summary>
    /// 代码生成器。参考自：Zxw.Framework.NetCore
    /// <remarks>
    /// 根据数据库表以及表对应的列生成对应的数据库实体
    /// </remarks>
    /// </summary>
    public class CodeGenerator
    {
        private readonly string Delimiter = "\\";//分隔符，默认为windows下的\\分隔符

        private readonly CodeGenerateSettings _options;
        public CodeGenerator(IOptions<CodeGenerateSettings> options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            _options = options.Value;
            if (_options.ConnectionString.IsNullOrWhiteSpace())
                throw new ArgumentNullException("不指定【数据库连接串】就生成代码，你想上天吗？");
            if (_options.DbType.IsNullOrWhiteSpace())
                throw new ArgumentNullException("不指定【数据库类型】就生成代码，你想逆天吗？");
            var path = AppDomain.CurrentDomain.BaseDirectory;
            if (_options.OutputPath.IsNullOrWhiteSpace())
                _options.OutputPath = path;
            var flag = path.IndexOf("/bin");
            if (flag > 0)
                Delimiter = "/";//如果可以取到值，修改分割符
        }

        /// <summary>
        /// 根据数据库连接字符串生成数据库表对应的模板代码
        /// </summary>
        /// <param name="isCoveredExsited">是否覆盖已存在的同名文件</param>
        public void GenerateTemplateCodesFromDatabase(bool isCoveredExsited = true)
        {
            DatabaseType dbType = DbHelper.GetDataBaseType(_options.DbType);
            List<DbTable> tables = new List<DbTable>();
            using (var dbConnection = DbHelper.CreateConnection(dbType, _options.ConnectionString))
            {
                tables = dbConnection.GetCurrentDatabaseTableList(dbType);
            }

            if (tables != null && tables.Any())
            {
                foreach (var table in tables)
                {
                    table.ClassName = "";
                    var arrClassName = table.TableName.Split("_");
                    if(arrClassName.Length > 1)
                    {
                        foreach (var item in arrClassName)
                        {
                            table.ClassName += item.UpperFirstChar();
                        }
                    }
                    else
                    {
                        table.ClassName = table.TableName.UpperFirstChar();
                    }
                    
                    GenerateEntity(table, isCoveredExsited);
                    GenerateConfiguration(table, isCoveredExsited);
                    GenerateIRepository(table, isCoveredExsited);
                    GenerateRepository(table, isCoveredExsited);
                    GenerateResource(table, isCoveredExsited);
                    GenerateParameters(table, isCoveredExsited);
                    //GenerateIServices(table, isCoveredExsited);
                    //GenerateServices(table, isCoveredExsited);
                }
                //GenerateDBContext(tables, isCoveredExsited);
            }
        }

        /// <summary>
        /// 生成实体代码
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="isCoveredExsited">是否覆盖</param>
        private void GenerateEntity(DbTable table, bool isCoveredExsited = true)
        {
            var modelPath = _options.OutputPath + Delimiter + "Models";
            if (!Directory.Exists(modelPath))
            {
                Directory.CreateDirectory(modelPath);
            }

            var fullPath = modelPath + Delimiter + table.ClassName + ".cs";
            if (File.Exists(fullPath) && !isCoveredExsited) return;

            var pkTypeName = table.Columns.First(m => m.IsPrimaryKey).CSharpType;
            var sb = new StringBuilder();
            foreach (var column in table.Columns)
            {
                var tmp = GenerateEntityProperty(column);//table.TableName, 
                sb.Append(tmp);
            }
            var content = ReadTemplate("ModelTemplate.txt");
            content = content.Replace("{TemplateNamespace}", _options.ModelsNamespace)
                .Replace("{Comment}", table.TableComment)
                .Replace("{ClassName}", table.ClassName)
                .Replace("{TemplateContent}", sb.ToString());

            WriteAndSave(fullPath, content);
        }

        /// <summary>
        /// 生成资源实体代码
        /// </summary>
        /// <param name="table"></param>
        /// <param name="isCoveredExsited"></param>
        private void GenerateResource(DbTable table, bool isCoveredExsited = true)
        {
            var modelPath = _options.OutputPath + Delimiter + "ModelResources";
            if (!Directory.Exists(modelPath))
            {
                Directory.CreateDirectory(modelPath);
            }

            var fullPath = modelPath + Delimiter + table.ClassName + "Resource.cs";
            if (File.Exists(fullPath) && !isCoveredExsited) return;

            var pkTypeName = table.Columns.First(m => m.IsPrimaryKey).CSharpType;
            var sb = new StringBuilder();
            foreach (var column in table.Columns)
            {
                var tmp = GenerateEntityProperty(column, true);//table.TableName, 
                sb.AppendLine(tmp);
            }

            var content = ReadTemplate("ResourceTemplate.txt");
            content = content.Replace("{TemplateNamespace}", _options.ResourcesNamespace)
                .Replace("{ClassName}", table.ClassName)
                .Replace("{TemplateContent}", sb.ToString());

            WriteAndSave(fullPath, content);
        }

        /// <summary>
        /// 生成查询参数表
        /// </summary>
        /// <param name="table"></param>
        /// <param name="isCoveredExsited"></param>
        private void GenerateParameters(DbTable table, bool isCoveredExsited = true)
        {
            var modelPath = _options.OutputPath + Delimiter + "ModelParameters";
            if (!Directory.Exists(modelPath))
            {
                Directory.CreateDirectory(modelPath);
            }

            var fullPath = modelPath + Delimiter + table.ClassName + "Parameter.cs";
            if (File.Exists(fullPath) && !isCoveredExsited) return;

            var content = ReadTemplate("ParametersTemplate.txt");
            content = content.Replace("{TemplateNamespace}", _options.ModelsNamespace)
                .Replace("{ClassName}", table.ClassName);

            WriteAndSave(fullPath, content);
        }

        /// <summary>
        /// 生成 DBContext
        /// </summary>
        /// <param name="tables"></param>
        /// <param name="ifExsitedCovered"></param>
        /// <returns></returns>
        private void GenerateDBContext(List<DbTable> tables, bool isCoveredExsited = true)
        {
            var dbContextPath = _options.OutputPath;
            if (!Directory.Exists(dbContextPath))
            {
                Directory.CreateDirectory(dbContextPath);
            }
            var fullPath = dbContextPath + Delimiter + "MyContext.cs";
            if (File.Exists(fullPath) && !isCoveredExsited) return;

            var sb = new StringBuilder();
            var sb2 = new StringBuilder();
            var sb3 = new StringBuilder();
            sb.AppendLine();
            sb2.AppendLine();
            sb3.AppendLine();
            foreach (var table in tables)
            {
                sb.AppendLine($"\t\t\tpublic virtual DbSet<{table.ClassName}> {table.TableName} {{ get; set; }}");
                sb2.AppendLine($"modelBuilder.ApplyConfiguration(new {table.ClassName}Configuration());");
                sb3.AppendLine($"//services.AddScoped<I{table.ClassName}Repository, {table.ClassName}Repository>();");
            }

            var content = ReadTemplate("DBContextTemplate.txt");
            content = content.Replace("{TemplateNamespace}", _options.DbContextNamespace)
                .Replace("{DbSetModelProperties}", sb.ToString())
                .Replace("{OnModelCreats}", sb2.ToString())
                .Replace("{TemplateAddScopeds}", sb3.ToString());

            WriteAndSave(fullPath, content);
        }

        /// <summary>
        /// 生成 Configurations
        /// </summary>
        /// <param name="tables"></param>
        /// <param name="ifExsitedCovered"></param>
        /// <returns></returns>
        private void GenerateConfiguration(DbTable table, bool isCoveredExsited = true)
        {
            var dbContextPath = _options.OutputPath + Delimiter + "Database";
            if (!Directory.Exists(dbContextPath))
            {
                Directory.CreateDirectory(dbContextPath);
            }
            var fullPath = dbContextPath + Delimiter + table.ClassName + "Configuration.cs";
            if (File.Exists(fullPath) && !isCoveredExsited) return;

            var sb = new StringBuilder();
            sb.AppendLine();

            foreach (var column in table.Columns)
            {
                if (column.CSharpType.ToLower() == "string")
                {
                    sb.AppendLine($"\t\t\t\tentity.Property(x => x.{column.ColName}).HasDefaultValue(\"\").IsRequired();");
                }
                else if (column.CSharpType.ToLower() == "datetime")
                {
                    sb.AppendLine($"\t\t\t\tentity.Property(x => x.{column.ColName}).HasDefaultValue(DateTime.Now).IsRequired();");
                }
            }

            var content = ReadTemplate("ConfigurationTemplate.txt");
            content = content.Replace("{TemplateNamespace}", _options.DbContextNamespace)
                .Replace("{ClassName}", table.ClassName).Replace("{TemplateColumns}", sb.ToString());

            WriteAndSave(fullPath, content);
        }

        /// <summary>
        /// 生成属性
        /// </summary>
        /// <param name="column">列</param>
        /// <param name="isContainskey">生成是否包含主键</param>
        /// <returns></returns>
        private static string GenerateEntityProperty(DbTableColumn column, bool isContainskey = false)
        {
            var sb = new StringBuilder();

            if (column.IsPrimaryKey && isContainskey)
            {
                if (!string.IsNullOrEmpty(column.Comment))
                {
                    sb.AppendLine("\t\t/// <summary>");
                    sb.AppendLine("\t\t/// " + column.Comment);
                    sb.AppendLine("\t\t/// </summary>");
                }
                //if (column.IsIdentity)
                //{
                //    sb.AppendLine("\t\t[DatabaseGenerated(DatabaseGeneratedOption.Identity)]");
                //}
                sb.AppendLine($"\t\tpublic {column.CSharpType} Id " + "{ get; set; }");
            }

            if (!column.IsPrimaryKey)
            {
                if (!string.IsNullOrEmpty(column.Comment))
                {
                    sb.AppendLine("\t\t/// <summary>");
                    sb.AppendLine("\t\t/// " + column.Comment);
                    sb.AppendLine("\t\t/// </summary>");
                }

                var colType = column.CSharpType;
                sb.AppendLine($"\t\tpublic {colType} {column.ColName} " + "{ get; set; }");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 生成IRepository层代码文件
        /// </summary>
        /// <param name="modelTypeName"></param>
        /// <param name="keyTypeName"></param>
        /// <param name="ifExsitedCovered"></param>
        private void GenerateIRepository(DbTable table, bool ifExsitedCovered = true)
        {
            var iRepositoryPath = _options.OutputPath + Delimiter + "IRepository";
            if (!Directory.Exists(iRepositoryPath))
            {
                Directory.CreateDirectory(iRepositoryPath);
            }
            var fullPath = iRepositoryPath + Delimiter + "I" + table.ClassName + "Repository.cs";
            if (File.Exists(fullPath) && !ifExsitedCovered)
                return;
            var content = ReadTemplate("IRepositoryTemplate.txt");
            content = content.Replace("{Comment}", table.TableComment)
                .Replace("{IRepositoryNamespace}", _options.IRepositoryNamespace)
                .Replace("{ClassName}", table.ClassName);

            WriteAndSave(fullPath, content);
        }

        /// <summary>
        /// 生成Repository层代码文件
        /// </summary>
        /// <param name="modelTypeName"></param>
        /// <param name="keyTypeName"></param>
        /// <param name="ifExsitedCovered"></param>
        private void GenerateRepository(DbTable table, bool ifExsitedCovered = true)
        {
            var repositoryPath = _options.OutputPath + Delimiter + "Repository";
            if (!Directory.Exists(repositoryPath))
            {
                Directory.CreateDirectory(repositoryPath);
            }
            var fullPath = repositoryPath + Delimiter + table.TableName + "Repository.cs";
            if (File.Exists(fullPath) && !ifExsitedCovered)
                return;
            var content = ReadTemplate("RepositoryTemplate.txt");
            content = content.Replace("{Comment}", table.TableComment)
                .Replace("{RepositoryNamespace}", _options.RepositoryNamespace)
                .Replace("{TableName}", table.TableName)
                .Replace("{ClassName}", table.ClassName);

            WriteAndSave(fullPath, content);
        }

        /// <summary>
        /// 生成IService层代码文件
        /// </summary>
        /// <param name="modelTypeName"></param>
        /// <param name="keyTypeName"></param>
        /// <param name="ifExsitedCovered"></param>
        private void GenerateIServices(DbTable table, bool ifExsitedCovered = true)
        {
            var iServicesPath = _options.OutputPath + Delimiter + "IServices";
            if (!Directory.Exists(iServicesPath))
            {
                Directory.CreateDirectory(iServicesPath);
            }
            var fullPath = iServicesPath + Delimiter + "I" + table.TableName + "Service.cs";
            if (File.Exists(fullPath) && !ifExsitedCovered)
                return;
            var content = ReadTemplate("IServicesTemplate.txt");
            content = content.Replace("{Comment}", table.TableComment)
                .Replace("{IServicesNamespace}", _options.IServicesNamespace)
                .Replace("{ModelName}", table.TableName);
            WriteAndSave(fullPath, content);
        }

        /// <summary>
        /// 生成Services层代码文件
        /// </summary>
        /// <param name="modelTypeName"></param>
        /// <param name="keyTypeName"></param>
        /// <param name="ifExsitedCovered"></param>
        private void GenerateServices(DbTable table, bool ifExsitedCovered = true)
        {
            var repositoryPath = _options.OutputPath + Delimiter + "Services";
            if (!Directory.Exists(repositoryPath))
            {
                Directory.CreateDirectory(repositoryPath);
            }
            var fullPath = repositoryPath + Delimiter + table.TableName + "Service.cs";
            if (File.Exists(fullPath) && !ifExsitedCovered)
                return;
            var content = ReadTemplate("ServiceTemplate.txt");
            content = content.Replace("{Comment}", table.TableComment)
                .Replace("{ServicesNamespace}", _options.ServicesNamespace)
                .Replace("{ModelName}", table.TableName);
            WriteAndSave(fullPath, content);
        }

        /// <summary>
        /// 从代码模板中读取内容
        /// </summary>
        /// <param name="templateName">模板名称，应包括文件扩展名称。比如：template.txt</param>
        /// <returns></returns>
        private string ReadTemplate(string templateName)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();//获取当前正在执行的程序集
            var content = string.Empty;
            using (var stream = currentAssembly.GetManifestResourceStream($"{currentAssembly.GetName().Name}.CodeGenerator.CodeTemplates.{templateName}"))
            {
                if (stream != null)
                {
                    using (var reader = new StreamReader(stream))
                    {
                        content = reader.ReadToEnd();
                    }
                }
            }
            return content;
        }

        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="fileName">文件完整路径</param>
        /// <param name="content">内容</param>
        private static void WriteAndSave(string fileName, string content)
        {
            //实例化一个文件流--->与写入文件相关联
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                //实例化一个StreamWriter-->与fs相关联
                using (var sw = new StreamWriter(fs))
                {
                    //开始写入
                    sw.Write(content);
                    //清空缓冲区
                    sw.Flush();
                    //关闭流
                    sw.Close();
                    fs.Close();
                }
            }
        }
    }
}
