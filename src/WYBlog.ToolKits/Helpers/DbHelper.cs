using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SqlClient;
using WYBlog.Enums;

namespace WYBlog.Helpers
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
            if (string.IsNullOrWhiteSpace(dbtype))
                throw new ArgumentNullException("获取数据库连接需要传数据库类型！");
            if (string.IsNullOrWhiteSpace(strConn))
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
        public static IDbConnection CreateConnection(DbTypeEnum dbType, string strConn)
        {
            IDbConnection connection = null;
            if (string.IsNullOrWhiteSpace(strConn))
                throw new ArgumentNullException("获取数据库连接需要传数据库连接字符串！");

            switch (dbType)
            {
                case DbTypeEnum.MySql:
                    connection = new MySqlConnection(strConn);
                    break;
                case DbTypeEnum.SqlServer:
                    connection = new SqlConnection(strConn);
                    break;
                default:
                    throw new ArgumentNullException($"对不起，暂时还不支持的{dbType}类型的数据库！");
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
        public static DbTypeEnum GetDataBaseType(string dbtype)
        {
            if (dbtype.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException("获取数据库连接需要穿数据库类型。");
            }

            DbTypeEnum returnValue = DbTypeEnum.MySql;
            foreach (DbTypeEnum dbType in Enum.GetValues(typeof(DbTypeEnum)))
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