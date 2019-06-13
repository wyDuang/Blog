using MySql.Data.MySqlClient;
using System;
using System.Data;
using Blog.Infrastructure.CodeGenerator.CodeGenerateModels;
using Blog.Infrastructure.Extensions;

namespace Blog.Infrastructure.Helpers
{
    /// <summary>
    /// 数据库连接工厂类
    /// </summary>
    public class DbHelper
    {
        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="dbtype">数据库类型</param>
        /// <param name="strConn">数据库连接字符串</param>
        /// <returns>返回数据库连接</returns>
        public static IDbConnection CreateConnection(string dbtype, string strConn)
        {
            if (dbtype.IsNullOrWhiteSpace())
                throw new ArgumentNullException("获取数据库连接需要传数据库类型！");
            if (strConn.IsNullOrWhiteSpace())
                throw new ArgumentNullException("获取数据库连接需要传数据库连接字符串！");

            var dbType = GetDataBaseType(dbtype);
            return CreateConnection(dbType, strConn);
        }

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="dbType">数据库枚举类型</param>
        /// <param name="strConn">数据库连接字符串</param>
        /// <returns>数据库连接</returns>
        public static IDbConnection CreateConnection(DatabaseType dbType, string strConn)
        {
            IDbConnection connection = null;
            if (strConn.IsNullOrWhiteSpace())
                throw new ArgumentNullException("获取数据库连接需要传数据库连接字符串！");

            switch (dbType)
            {
                case DatabaseType.MySQL:
                    connection = new MySqlConnection(strConn);
                    break;
                //case DatabaseType.SqlServer:
                //    connection = new SqlConnection(strConn);
                //    break;
                default:
                    throw new ArgumentNullException($"对不起，暂时还不支持的{dbType.ToString()}类型的数据库！");
            }

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            return connection;
        }

        /// <summary>
        /// 转换数据库类型
        /// </summary>
        /// <param name="dbtype">数据库类型字符串</param>
        /// <returns>数据库类型</returns>
        public static DatabaseType GetDataBaseType(string dbtype)
        {
            if (dbtype.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException("获取数据库连接需要穿数据库类型。");
            }

            DatabaseType returnValue = DatabaseType.MySQL;
            foreach (DatabaseType dbType in Enum.GetValues(typeof(DatabaseType)))
            {
                if (dbType.ToString().Equals(dbtype, StringComparison.OrdinalIgnoreCase))
                {
                    returnValue = dbType;
                    break;
                }
            }
            return returnValue;
        }
    }
}
