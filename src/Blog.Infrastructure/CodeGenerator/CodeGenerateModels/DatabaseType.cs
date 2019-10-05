using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.CodeGenerator.CodeGenerateModels
{
    /// <summary>
    /// 数据库类型枚举
    /// </summary>
    public enum DatabaseType
    {
        SqlServer,
        MySQL,
        PostgreSQL,
        SQLite,
        InMemory,
        Oracle,
        MariaDB,
        MyCat,
        Firebird,
        DB2,
        Access
    }
}
