using System.Collections.Generic;
using WYBlog.Enums;

namespace WYBlog.CodeGenerator
{
    /// <summary>
    /// 数据库字段类型集合
    /// </summary>
    public class DbColumnTypeCollection
    {
        public static IList<DbColumnDataType> DbColumnDataTypes => new List<DbColumnDataType>()
        {
            #region SqlServer，https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/sql-server-data-type-mappings

            new DbColumnDataType(){ DbType = DbTypeEnum.SqlServer, DbColumnType = "bigint", CSharpType = "Int64"},
            new DbColumnDataType(){ DbType = DbTypeEnum.SqlServer, DbColumnType = "binary,image,varbinary(max),rowversion,timestamp,varbinary", CSharpType = "Byte[]"},
            new DbColumnDataType(){ DbType = DbTypeEnum.SqlServer, DbColumnType = "bit", CSharpType = "Boolean"},
            new DbColumnDataType(){ DbType = DbTypeEnum.SqlServer, DbColumnType = "char,nchar,text,ntext,varchar,nvarchar", CSharpType = "String"},
            new DbColumnDataType(){ DbType = DbTypeEnum.SqlServer, DbColumnType = "date,datetime,datetime2,smalldatetime", CSharpType ="DateTime"},
            new DbColumnDataType(){ DbType = DbTypeEnum.SqlServer, DbColumnType = "datetimeoffset", CSharpType ="DateTimeOffset"},
            new DbColumnDataType(){ DbType = DbTypeEnum.SqlServer, DbColumnType = "decimal,money,numeric,smallmoney", CSharpType ="Decimal"},
            new DbColumnDataType(){ DbType = DbTypeEnum.SqlServer, DbColumnType = "float", CSharpType ="Double"},
            new DbColumnDataType(){ DbType = DbTypeEnum.SqlServer, DbColumnType = "int", CSharpType ="Int32"},
            new DbColumnDataType(){ DbType = DbTypeEnum.SqlServer, DbColumnType = "real", CSharpType ="Single"},
            new DbColumnDataType(){ DbType = DbTypeEnum.SqlServer, DbColumnType = "smallint", CSharpType ="Int16"},
            new DbColumnDataType(){ DbType = DbTypeEnum.SqlServer, DbColumnType = "sql_variant", CSharpType ="Object"},
            new DbColumnDataType(){ DbType = DbTypeEnum.SqlServer, DbColumnType = "time", CSharpType ="TimeSpan"},
            new DbColumnDataType(){ DbType = DbTypeEnum.SqlServer, DbColumnType = "tinyint", CSharpType ="Byte"},
            new DbColumnDataType(){ DbType = DbTypeEnum.SqlServer, DbColumnType = "uniqueidentifier", CSharpType ="Guid"},

            #endregion SqlServer，https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/sql-server-data-type-mappings

            #region PostgreSQL，http://www.npgsql.org/doc/types/basic.html

            new DbColumnDataType(){ DbType = DbTypeEnum.PostgreSql, DbColumnType = "boolean,bit(1)", CSharpType ="Boolean"},
            new DbColumnDataType(){ DbType = DbTypeEnum.PostgreSql, DbColumnType = "smallint", CSharpType ="short"},
            new DbColumnDataType(){ DbType = DbTypeEnum.PostgreSql, DbColumnType = "integer", CSharpType ="int"},
            new DbColumnDataType(){ DbType = DbTypeEnum.PostgreSql, DbColumnType = "bigint", CSharpType ="long"},
            new DbColumnDataType(){ DbType = DbTypeEnum.PostgreSql, DbColumnType = "real", CSharpType ="float"},
            new DbColumnDataType(){ DbType = DbTypeEnum.PostgreSql, DbColumnType = "double precision", CSharpType ="Double"},
            new DbColumnDataType(){ DbType = DbTypeEnum.PostgreSql, DbColumnType = "numeric,money", CSharpType ="decimal"},
            new DbColumnDataType(){ DbType = DbTypeEnum.PostgreSql, DbColumnType = "text,character,character varying,citext,json,jsonb,xml,name", CSharpType ="String"},
            new DbColumnDataType(){ DbType = DbTypeEnum.PostgreSql, DbColumnType = "point", CSharpType ="NpgsqlTypes.NpgsqlPoint"},
            new DbColumnDataType(){ DbType = DbTypeEnum.PostgreSql, DbColumnType = "lseg", CSharpType ="NpgsqlTypes.NpgsqlLSeg"},
            new DbColumnDataType(){ DbType = DbTypeEnum.PostgreSql, DbColumnType = "path", CSharpType ="NpgsqlTypes.NpgsqlPath"},
            new DbColumnDataType(){ DbType = DbTypeEnum.PostgreSql, DbColumnType = "polygon", CSharpType ="NpgsqlTypes.NpgsqlPolygon"},
            new DbColumnDataType(){ DbType = DbTypeEnum.PostgreSql, DbColumnType = "line", CSharpType ="NpgsqlTypes.NpgsqlLine"},
            new DbColumnDataType(){ DbType = DbTypeEnum.PostgreSql, DbColumnType = "circle", CSharpType ="NpgsqlTypes.NpgsqlCircle"},
            new DbColumnDataType(){ DbType = DbTypeEnum.PostgreSql, DbColumnType = "box", CSharpType ="NpgsqlTypes.NpgsqlBox"},
            new DbColumnDataType(){ DbType = DbTypeEnum.PostgreSql, DbColumnType = "bit(n),bit varying", CSharpType ="BitArray"},
            new DbColumnDataType(){ DbType = DbTypeEnum.PostgreSql, DbColumnType = "hstore", CSharpType ="IDictionary<string, string>"},
            new DbColumnDataType(){ DbType = DbTypeEnum.PostgreSql, DbColumnType = "uuid", CSharpType ="Guid"},
            new DbColumnDataType(){ DbType = DbTypeEnum.PostgreSql, DbColumnType = "cidr", CSharpType ="ValueTuple<IPAddress, int>"},
            new DbColumnDataType(){ DbType = DbTypeEnum.PostgreSql, DbColumnType = "inet", CSharpType ="IPAddress"},
            new DbColumnDataType(){ DbType = DbTypeEnum.PostgreSql, DbColumnType = "macaddr", CSharpType ="PhysicalAddress"},
            new DbColumnDataType(){ DbType = DbTypeEnum.PostgreSql, DbColumnType = "tsquery", CSharpType ="NpgsqlTypes.NpgsqlTsQuery"},
            new DbColumnDataType(){ DbType = DbTypeEnum.PostgreSql, DbColumnType = "tsvector", CSharpType ="NpgsqlTypes.NpgsqlTsVector"},
            new DbColumnDataType(){ DbType = DbTypeEnum.PostgreSql, DbColumnType = "date,timestamp,timestamp with time zone,timestamp without time zone", CSharpType ="DateTime"},
            new DbColumnDataType(){ DbType = DbTypeEnum.PostgreSql, DbColumnType = "interval,time", CSharpType ="TimeSpan"},
            new DbColumnDataType(){ DbType = DbTypeEnum.PostgreSql, DbColumnType = "time with time zone", CSharpType ="DateTimeOffset"},
            new DbColumnDataType(){ DbType = DbTypeEnum.PostgreSql, DbColumnType = "bytea", CSharpType ="Byte[]"},
            new DbColumnDataType(){ DbType = DbTypeEnum.PostgreSql, DbColumnType = "oid,xid,cid", CSharpType ="uint"},
            new DbColumnDataType(){ DbType = DbTypeEnum.PostgreSql, DbColumnType = "oidvector", CSharpType ="uint[]"},
            new DbColumnDataType(){ DbType = DbTypeEnum.PostgreSql, DbColumnType = "char", CSharpType ="char"},
            new DbColumnDataType(){ DbType = DbTypeEnum.PostgreSql, DbColumnType = "geometry", CSharpType ="NpgsqlTypes.PostgisGeometry"},
            new DbColumnDataType(){ DbType = DbTypeEnum.PostgreSql, DbColumnType = "record", CSharpType ="object[]"},

            #endregion PostgreSQL，http://www.npgsql.org/doc/types/basic.html

            #region MySQL，https://www.devart.com/dotconnect/mysql/docs/DataTypeMapping.html

            new DbColumnDataType(){ DbType = DbTypeEnum.MySql, DbColumnType = "bool,boolean,bit(1),tinyint(1)", CSharpType ="Boolean"},
            new DbColumnDataType(){ DbType = DbTypeEnum.MySql, DbColumnType = "tinyint", CSharpType ="SByte"},
            new DbColumnDataType(){ DbType = DbTypeEnum.MySql, DbColumnType = "tinyint unsigned", CSharpType ="Byte"},
            new DbColumnDataType(){ DbType = DbTypeEnum.MySql, DbColumnType = "smallint, year", CSharpType ="Int16"},
            new DbColumnDataType(){ DbType = DbTypeEnum.MySql, DbColumnType = "int, integer, smallint unsigned, mediumint", CSharpType ="Int32"},
            new DbColumnDataType(){ DbType = DbTypeEnum.MySql, DbColumnType = "bigint, int unsigned, integer unsigned, bit", CSharpType ="Int64"},
            new DbColumnDataType(){ DbType = DbTypeEnum.MySql, DbColumnType = "float", CSharpType ="Single"},
            new DbColumnDataType(){ DbType = DbTypeEnum.MySql, DbColumnType = "double, real", CSharpType ="Double"},
            new DbColumnDataType(){ DbType = DbTypeEnum.MySql, DbColumnType = "decimal, numeric, dec, fixed, bigint unsigned, float unsigned, double unsigned, serial", CSharpType ="Decimal"},
            new DbColumnDataType(){ DbType = DbTypeEnum.MySql, DbColumnType = "date, timestamp, datetime", CSharpType ="DateTime"},
            new DbColumnDataType(){ DbType = DbTypeEnum.MySql, DbColumnType = "datetimeoffset", CSharpType ="DateTimeOffset"},
            new DbColumnDataType(){ DbType = DbTypeEnum.MySql, DbColumnType = "time", CSharpType ="TimeSpan"},
            new DbColumnDataType(){ DbType = DbTypeEnum.MySql, DbColumnType = "char, varchar, tinytext, text, mediumtext, longtext, set, enum, nchar, national char, nvarchar, national varchar, character varying", CSharpType ="String"},
            new DbColumnDataType(){ DbType = DbTypeEnum.MySql, DbColumnType = "binary, varbinary, tinyblob, blob, mediumblob, longblob, char byte", CSharpType ="Byte[]"},
            new DbColumnDataType(){ DbType = DbTypeEnum.MySql, DbColumnType = "geometry", CSharpType ="System.Data.Spatial.DbGeometry"},
            new DbColumnDataType(){ DbType = DbTypeEnum.MySql, DbColumnType = "geometry", CSharpType ="System.Data.Spatial.DbGeography"},

            #endregion MySQL，https://www.devart.com/dotconnect/mysql/docs/DataTypeMapping.html

            #region Oracle, https://docs.oracle.com/cd/E14435_01/win.111/e10927/featUDTs.htm#CJABAEDD

            new DbColumnDataType(){ DbType = DbTypeEnum.Oracle, DbColumnType = "BFILE,BLOB,RAW,LONG RAW", CSharpType ="Byte[]"},
            new DbColumnDataType(){ DbType = DbTypeEnum.Oracle, DbColumnType = "CHAR, NCHAR, VARCHAR2, CLOB, NCLOB,NVARCHAR2,REF,XMLTYPE,ROWID,LONG", CSharpType ="String"},
            new DbColumnDataType(){ DbType = DbTypeEnum.Oracle, DbColumnType = "BINARY FLOAT,BINARY DOUBLE", CSharpType ="System.Byte"},
            new DbColumnDataType(){ DbType = DbTypeEnum.Oracle, DbColumnType = "INTERVAL YEAR TO MONTH", CSharpType ="Int32"},
            new DbColumnDataType(){ DbType = DbTypeEnum.Oracle, DbColumnType = "FLOAT,INTEGER,NUMBER", CSharpType ="Decimal"},
            new DbColumnDataType(){ DbType = DbTypeEnum.Oracle, DbColumnType = "DATE, TIMESTAMP, TIMESTAMP WITH LOCAL TIME ZONE,TIMESTAMP WITH TIME ZONE", CSharpType ="DateTime"},
            new DbColumnDataType(){ DbType = DbTypeEnum.Oracle, DbColumnType = "INTERVAL DAY TO SECOND", CSharpType ="TimeSpan"},

            #endregion Oracle, https://docs.oracle.com/cd/E14435_01/win.111/e10927/featUDTs.htm#CJABAEDD
        };
    }
}