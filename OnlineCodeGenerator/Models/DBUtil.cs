using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.SqlServer.Management.Smo;
using System.Collections.Generic;

namespace OnlineCodeGenerator.Models
{
    /// <summary>
    /// 数据库工具
    /// </summary>
    public class DBUtil
    {
        #region Singleton
        /// <summary>
        /// 工具
        /// </summary>
        private static readonly DBUtil util = new DBUtil();

        /// <summary>
        /// 私有构造函数
        /// </summary>
        private DBUtil() { }

        /// <summary>
        /// 工具实例
        /// </summary>
        public static DBUtil Util
        {
            get
            {
                return util;
            }
        }
        #endregion

        #region 获得数据库服务对象 +Server GetDBServer(DBModel db)
        /// <summary>
        /// 获得数据库服务对象
        /// </summary>
        /// <param name="db">数据库信息</param>
        /// <returns></returns>
        private Server GetDBServer(DBModel db)
        {
            Server server = new Server(db.IP);
            server.ConnectionContext.LoginSecure = false;
            server.ConnectionContext.Login = db.UserName;
            server.ConnectionContext.Password = db.Password;
            server.ConnectionContext.Connect();
            return server;
        }
        #endregion

        #region 获得连接下的所有数据库 +Server GetDatabases(DBModel db)
        /// <summary>
        /// 获得连接下的所有数据库
        /// </summary>
        /// <param name="db">数据库信息</param>
        /// <returns>返回数据库服务对象</returns>
        public Server GetDatabases(DBModel db)
        {
            Server server = GetDBServer(db);
            db.DbList = new List<SelectListItem>();
            foreach (Database item in server.Databases)
            {
                db.DbList.Add(new SelectListItem
                {
                    Value = item.Name,
                    Text = item.Name,
                    Selected = string.Equals(db.Database, item.Name, System.StringComparison.OrdinalIgnoreCase)
                });
            }
            return server;
        }
        #endregion

        #region 获得指定数据库下的所有数据表 +void GetTables(DBModel db)
        /// <summary>
        /// 获得指定数据库下的所有数据表
        /// </summary>
        /// <param name="db">数据库信息</param>
        public void GetTables(DBModel db)
        {
            Server server = GetDatabases(db);
            db.TableList = new List<SelectListItem>();
            Database database = server.Databases[db.Database];
            foreach (Table item in database.Tables)
            {
                db.TableList.Add(new SelectListItem
                {
                    Value = item.Name,
                    Text = item.Name,
                    Selected = string.Equals(db.Database, item.Name, System.StringComparison.OrdinalIgnoreCase)
                });
            }
        }
        #endregion

        #region 构建实体类 +Poco BuildPoco(DBModel db)
        /// <summary>
        /// 构建实体类
        /// </summary>
        /// <param name="db">数据库信息</param>
        /// <returns></returns>
        public Poco BuildPoco(DBModel db)
        {
            Server server = GetDBServer(db);
            Table table = server.Databases[db.Database].Tables[db.Table];
            Poco poco = new Poco
            {
                NameSpace = db.CodeNameSpace,
                ClassName = table.Name,
                ClassComment = table.ExtendedProperties["MS_Description"] == null ? string.Empty : table.ExtendedProperties["MS_Description"].Value.ToString()
            };
            foreach (Column item in table.Columns)
            {
                string propertyType = GetNetDataType(item.DataType.Name);
                if (string.IsNullOrWhiteSpace(propertyType))
                {
                    continue;
                }
                PocoProperty property = new PocoProperty();
                if (item.Nullable && string.Equals(propertyType, "DateTime"))
                {
                    propertyType += "?";
                }
                property.Comment = item.ExtendedProperties["MS_Description"] == null ? string.Empty : item.ExtendedProperties["MS_Description"].Value.ToString();
                property.Name = item.Name;
                property.TypeName = propertyType;
                poco.Properties.Add(property);
            }
            return poco;
        }
        #endregion

        #region 获得.NET数据类型 -string GetNetDataType(string sqlDataTypeName)
        /// <summary>
        /// 获得.NET数据类型
        /// </summary>
        /// <param name="sqlDataTypeName">数据库数据类型</param>
        /// <returns></returns>
        private string GetNetDataType(string sqlDataTypeName)
        {
            switch (sqlDataTypeName.ToLower())
            {
                case "bigint":
                    return "Int64";
                case "binary":
                case "image":
                case "varbinary":
                    return "byte[]";
                case "bit":
                    return "bool";
                case "char":
                    return "char";
                case "datetime":
                case "smalldatetime":
                    return "DateTime";
                case "decimal":
                case "money":
                case "numeric":
                    return "decimal";
                case "float":
                    return "double";
                case "int":
                    return "int";
                case "nchar":
                case "nvarchar":
                case "text":
                case "varchar":
                case "xml":
                    return "string";
                case "real":
                    return "single";
                case "smallint":
                    return "Int16";
                case "tinyint":
                    return "byte";
                case "uniqueidentifier":
                    return "Guid";
                default:
                    return null;
            }
        }
        #endregion
    }
}
