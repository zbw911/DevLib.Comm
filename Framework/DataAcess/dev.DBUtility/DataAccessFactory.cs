// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：DataAccessFactory.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System.Configuration;

namespace Dev.DBUtility
{
    /// <summary>
    /// 数据库访问工厂
    /// </summary>
    public sealed class DataAccessFactory
    {
        private static DatabaseProperty _defaultDatabaseProperty;

        /// <summary>
        /// 数据库类型
        /// </summary>
        public static readonly string ConnDatabaseType = ConfigurationManager.AppSettings["DatabaseType"];

        /// <summary>
        /// 数据库连接字符串前台
        /// </summary>
        public static readonly string DBConnString = ConfigurationManager.ConnectionStrings["connectionString"] != null
                                                         ? ConfigurationManager.ConnectionStrings["connectionString"].
                                                               ConnectionString
                                                         : "";

        /// <summary>
        /// 数据库连接字符串2
        /// </summary>
        public static readonly string DBConnString2 = ConfigurationManager.ConnectionStrings["connectionString2"] !=
                                                      null
                                                          ? ConfigurationManager.ConnectionStrings["connectionString2"].
                                                                ConnectionString
                                                          : "";

        /// <summary>
        /// 数据库连接字符串3
        /// </summary>
        public static readonly string DBConnString3 = ConfigurationManager.ConnectionStrings["connectionString3"] !=
                                                      null
                                                          ? ConfigurationManager.ConnectionStrings["connectionString3"].
                                                                ConnectionString
                                                          : "";

        /// <summary>
        /// 数据库连接字符串4
        /// </summary>
        public static readonly string DBConnString4 = ConfigurationManager.ConnectionStrings["connectionString4"] !=
                                                      null
                                                          ? ConfigurationManager.ConnectionStrings["connectionString4"].
                                                                ConnectionString
                                                          : "";

        /// <summary>
        /// 数据库属性
        /// </summary>
        private static DatabaseProperty DefaultDatabaseProperty
        {
            get { return _defaultDatabaseProperty; }
            set { _defaultDatabaseProperty = value; }
        }


        /// <summary>
        /// 创建
        /// </summary>
        /// <returns></returns>
        public static IDataAccess CreateDataAccess()
        {
            _defaultDatabaseProperty.ConnectionString = DBConnString;
            string connDatabaseType = ConnDatabaseType;


            DatabaseType dbt;
            switch (connDatabaseType)
            {
                case null:
                    {
                        dbt = DatabaseType.MSSQLServer;
                        break;
                    }
                case "":
                    {
                        dbt = DatabaseType.MSSQLServer;
                        break;
                    }
                case "Sql":
                    {
                        dbt = DatabaseType.MSSQLServer;
                        break;
                    }
                case "Ora":
                    {
                        dbt = DatabaseType.Oracle;
                        break;
                    }
                case "Ole":
                    {
                        dbt = DatabaseType.OleDBSupported;
                        break;
                    }
                default:
                    {
                        dbt = DatabaseType.MSSQLServer;
                        break;
                    }
            }
            _defaultDatabaseProperty.DatabaseType = dbt;
            return CreateDataAccess(_defaultDatabaseProperty);
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="pp">数据库属性</param>
        /// <returns></returns>
        private static IDataAccess CreateDataAccess(DatabaseProperty pp)
        {
            switch (pp.DatabaseType)
            {
                case DatabaseType.MSSQLServer:
                    return new MSSqlDataAccess(pp.ConnectionString);

                case DatabaseType.Oracle:
                    return new OracleDataAccess(pp.ConnectionString);

                case DatabaseType.OleDBSupported:
                    return new OleDbDataAccess(pp.ConnectionString);
            }
            return new MSSqlDataAccess(pp.ConnectionString);
        }

        /// <summary>
        /// 生成数据库接口,默认的是 MS-SQL
        /// </summary>
        /// <param name="ConnectionString"></param>
        /// <returns></returns>
        public static IDataAccess CreateDataAccess(string ConnectionString)
        {
            var dp = new DatabaseProperty();
            dp.DatabaseType = DatabaseType.MSSQLServer;
            dp.ConnectionString = ConnectionString;

            return CreateDataAccess(dp);
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="DBType"></param>
        /// <param name="DBConnStr"></param>
        /// <returns></returns>
        public static IDataAccess CreateDataAccess(string DBType, int DBConnStr)
        {
            switch (DBConnStr)
            {
                case 1:
                    _defaultDatabaseProperty.ConnectionString = DBConnString;
                    break;

                case 2:
                    _defaultDatabaseProperty.ConnectionString = DBConnString2;
                    break;

                case 3:
                    _defaultDatabaseProperty.ConnectionString = DBConnString3;
                    break;

                case 4:
                    _defaultDatabaseProperty.ConnectionString = DBConnString4;
                    break;

                default:
                    _defaultDatabaseProperty.ConnectionString = DBConnString;
                    break;
            }

            switch (DBType)
            {
                case "Sql":
                    {
                        _defaultDatabaseProperty.DatabaseType = DatabaseType.MSSQLServer;
                        break;
                    }
                case "Ora":
                    {
                        _defaultDatabaseProperty.DatabaseType = DatabaseType.Oracle;
                        break;
                    }
                case "Ole":
                    {
                        _defaultDatabaseProperty.DatabaseType = DatabaseType.OleDBSupported;
                        break;
                    }
                default:
                    {
                        _defaultDatabaseProperty.DatabaseType = DatabaseType.MSSQLServer;
                        break;
                    }
            }
            return CreateDataAccess(_defaultDatabaseProperty);
        }
    }
}