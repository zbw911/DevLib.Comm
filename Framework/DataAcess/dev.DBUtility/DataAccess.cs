// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：DataAccess.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System;
using System.Data;
using System.Xml;

namespace Dev.DBUtility
{
    /// <summary>
    /// 数据访问接口
    /// </summary>
    public interface IDataAccess : IDisposable
    {
        /// <summary>
        /// 数据类型
        /// </summary>
        DatabaseType DatabaseType { get; }

        /// <summary>
        ///数据库连接
        /// </summary>
        IDbConnection DbConnection { get; }

        /// <summary>
        /// 连接是否关闭
        /// </summary>
        bool IsClosed { get; }

        /// <summary>
        /// 指示是否进行SQL注入检查,在使用本方法的时候，1，你使用的不是拼装的SQL
        /// </summary>
        /// <param name="checkSql"></param>
        /// <returns></returns>
        /// 
        IDataAccess UseCheckSQL(bool checkSql);

        /// <summary>
        /// 是否检查参数
        /// </summary>
        /// <param name="checkParms"></param>
        /// <returns></returns>
        IDataAccess UseCheckParms(bool checkParms);

        /// <summary>
        /// 自动关闭,本方法要显示关闭或在 using {} 块中使用
        /// </summary>
        /// <returns></returns>
        IDataAccess KeepOpen(bool keep = true);

        /// <summary>
        /// 创建事务
        /// </summary>
        /// <returns></returns>
        IDataAccess BeginTransaction();

        /// <summary>
        /// 提交事务
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// 回滚事务
        /// </summary>
        void RollbackTransaction();

        /// <summary>
        /// 关闭连接
        /// </summary>
        void Close();

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        void Open();

        #region ExecuteDataset

        /// <summary>
        /// 执行操作返回 , DataSet
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        DataSet ExecuteDataset(string commandText);

        /// <summary>
        /// 执行操作返回 , DataSet
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        DataSet ExecuteDataset(CommandType commandType, string commandText);

        /// <summary>
        /// 执行操作返回 , DataSet
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        DataSet ExecuteDataset(string commandText, QueryParameterCollection commandParameters);

        /// <summary>
        /// 执行操作返回 , DataSet
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        DataSet ExecuteDataset(string commandText, DataSet ds);

        /// <summary>
        /// 执行操作返回 , DataSet
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        DataSet ExecuteDataset(string commandText, string tableName);

        /// <summary>
        /// 执行操作返回 , DataSet
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        DataSet ExecuteDataset(CommandType commandType, string commandText, QueryParameterCollection commandParameters);

        /// <summary>
        /// 执行操作返回 , DataSet
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        DataSet ExecuteDataset(CommandType commandType, string commandText, DataSet ds);

        /// <summary>
        /// 执行操作返回 , DataSet
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        DataSet ExecuteDataset(CommandType commandType, string commandText, string tableName);

        /// <summary>
        /// 执行操作返回 , DataSet
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        DataSet ExecuteDataset(string commandText, QueryParameterCollection commandParameters, DataSet ds);

        /// <summary>
        /// 执行操作返回 , DataSet
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        DataSet ExecuteDataset(string commandText, QueryParameterCollection commandParameters, string tableName);

        /// <summary>
        /// 执行操作返回 , DataSet
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="ds"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        DataSet ExecuteDataset(string commandText, DataSet ds, string tableName);

        /// <summary>
        /// 执行操作返回 , DataSet
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        DataSet ExecuteDataset(CommandType commandType, string commandText, QueryParameterCollection commandParameters,
                               DataSet ds);

        /// <summary>
        /// 执行操作返回 , DataSet
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        DataSet ExecuteDataset(CommandType commandType, string commandText, QueryParameterCollection commandParameters,
                               string tableName);

        /// <summary>
        /// 执行操作返回 , DataSet
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="ds"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        DataSet ExecuteDataset(CommandType commandType, string commandText, DataSet ds, string tableName);

        /// <summary>
        /// 执行操作返回 , DataSet
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <param name="ds"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        DataSet ExecuteDataset(string commandText, QueryParameterCollection commandParameters, DataSet ds,
                               string tableName);

        /// <summary>
        /// 执行操作返回 , DataSet
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <param name="ds"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        DataSet ExecuteDataset(CommandType commandType, string commandText, QueryParameterCollection commandParameters,
                               DataSet ds, string tableName);

        #endregion

        #region NonQuery

        /// <summary>
        /// 执行NonQuery 返回影响行数
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        int ExecuteNonQuery(string commandText);

        /// <summary>
        /// 执行NonQuery 返回影响行数
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        int ExecuteNonQuery(CommandType commandType, string commandText);

        /// <summary>
        /// 执行NonQuery 返回影响行数
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        int ExecuteNonQuery(string commandText, QueryParameterCollection commandParameters);

        /// <summary>
        /// /执行NonQuery 返回影响行数
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        int ExecuteNonQuery(CommandType commandType, string commandText, QueryParameterCollection commandParameters);

        #endregion

        #region 要手动关闭连接的Reader

        /// <summary>
        /// 执行Reader 返回 IDataReader , 要手动关闭连接
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        IDataReader ExecuteReader(string commandText);

        /// <summary>
        /// 执行Reader 返回 IDataReader , 要手动关闭连接
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        IDataReader ExecuteReader(CommandType commandType, string commandText);

        /// <summary>
        ///  执行Reader 返回 IDataReader , 要手动关闭连接
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        IDataReader ExecuteReader(string commandText, QueryParameterCollection commandParameters);

        /// <summary>
        ///  执行Reader 返回 IDataReader , 要手动关闭连接
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        IDataReader ExecuteReader(CommandType commandType, string commandText,
                                  QueryParameterCollection commandParameters);

        #endregion

        #region 自动关闭连接的Reader

        /// <summary>
        /// 执行Reader  连接与Reader自动关闭，要使用  action 事件进行处理
        /// </summary>
        /// <param name="commandText"></param>
        ///  <param name="action"></param>
        void ExecuteReader(Action<IDataReader> action, string commandText);

        /// <summary>
        /// 执行Reader  连接与Reader自动关闭，要使用  useReader 事件进行处理
        /// </summary>
        /// <param name="commandType"></param>
        ///  <param name="action"></param>
        /// <param name="commandText"></param>
        void ExecuteReader(Action<IDataReader> action, CommandType commandType, string commandText);

        /// <summary>
        /// 执行Reader  连接与Reader自动关闭，要使用  useReader 事件进行处理
        /// </summary>
        /// <param name="commandText"></param>
        ///   <param name="action"></param>
        /// <param name="commandParameters"></param>
        void ExecuteReader(Action<IDataReader> action, string commandText, QueryParameterCollection commandParameters);

        /// <summary>
        ///  执行Reader  连接与Reader自动关闭，要使用  useReader 事件进行处理
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="action"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        void ExecuteReader(Action<IDataReader> action, CommandType commandType, string commandText,
                           QueryParameterCollection commandParameters);

        #endregion

        #region ExecuteScalar

        /// <summary>
        ///  执行 Scalar
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        object ExecuteScalar(string commandText);

        /// <summary>
        /// 执行 Scalar
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        object ExecuteScalar(CommandType commandType, string commandText);

        /// <summary>
        /// 执行 Scalar
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        object ExecuteScalar(string commandText, QueryParameterCollection commandParameters);

        /// <summary>
        /// 执行 Scalar
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        object ExecuteScalar(CommandType commandType, string commandText, QueryParameterCollection commandParameters);

        #endregion

        #region XmlReader

        /// <summary>
        /// 执行 XmlReader 要手动关闭 READER
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        XmlReader ExecuteXmlReader(string commandText);

        /// <summary>
        /// 执行 XmlReader , 要手动关闭 READER
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        XmlReader ExecuteXmlReader(CommandType commandType, string commandText);

        /// <summary>
        /// 执行 XmlReader 要手动关闭 READER
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        XmlReader ExecuteXmlReader(string commandText, QueryParameterCollection commandParameters);

        /// <summary>
        /// 执行 XmlReader要手动关闭 READER
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        XmlReader ExecuteXmlReader(CommandType commandType, string commandText,
                                   QueryParameterCollection commandParameters);

        #endregion
    }
}