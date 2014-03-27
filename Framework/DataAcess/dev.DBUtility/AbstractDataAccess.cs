// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：AbstractDataAccess.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System;
using System.Data;
using System.Data.Common;
using System.Xml;

namespace Dev.DBUtility
{
    /// <summary>
    /// 数据库访问虚类
    /// </summary>
    public abstract class AbstractDataAccess : IDataAccess
    {
        /// <summary>
        /// 检查参数
        /// </summary>
        protected bool checkParms = true;

        /// <summary>
        /// 检查SQL
        /// </summary>
        protected bool checkSQL = true;

        /// <summary>
        /// 
        /// </summary>
        protected bool exceptioned = false;

        /// <summary>
        /// 
        /// </summary>
        protected bool keepopen = false;

        /// <summary>
        /// 连接
        /// </summary>
        protected DbConnection m_DbConnection;

        /// <summary>
        /// 事务
        /// </summary>
        protected DbTransaction trans;

        #region IDataAccess Members

        /// <summary>
        /// 开始事务
        /// </summary>
        /// <returns></returns>
        public IDataAccess BeginTransaction()
        {
            Open();
            trans = m_DbConnection.BeginTransaction();
            return this;
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void CommitTransaction()
        {
            if (trans != null)
            {
                trans.Commit();
                trans.Dispose();
                trans = null;
                m_DbConnection.Close();
            }
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void RollbackTransaction()
        {
            if (trans != null)
            {
                trans.Rollback();
                trans.Dispose();
            }
            trans = null;
            m_DbConnection.Close();
        }

        /// <summary>
        /// 关闭数库连接
        /// </summary>
        public void Close()
        {
            if (DbConnection.State.Equals(ConnectionState.Open) && trans == null && !keepopen)
            {
                DbConnection.Close();
            }
        }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public abstract DatabaseType DatabaseType { get; }

        /// <summary>
        /// 数据库连接
        /// </summary>
        public abstract IDbConnection DbConnection { get; }

        /// <summary>
        /// 是否关闭连接
        /// </summary>
        public bool IsClosed
        {
            get { return DbConnection.State.Equals(ConnectionState.Closed); }
        }


        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            keepopen = false;
            checkSQL = true;
            checkParms = true;
            if (exceptioned)
            {
                RollbackTransaction();
            }
            else
            {
                CommitTransaction();
            }
            Close();
        }

        /// <summary>
        /// 保持打开
        /// </summary>
        /// <param name="keep"></param>
        /// <returns></returns>
        public IDataAccess KeepOpen(bool keep = true)
        {
            keepopen = keep;
            return this;
        }

        /// <summary>
        /// 指示是否检查SQL
        /// </summary>
        /// <returns></returns>
        public IDataAccess UseCheckSQL(bool check)
        {
            throw new Exception("违反安全原则，异常从这里抛出");
            checkSQL = check;
            return this;
        }


        public IDataAccess UseCheckParms(bool checkParms)
        {
            throw new Exception("违反安全原则，异常从这里抛出");
            this.checkParms = checkParms;
            return this;
        }

        #endregion

        #region  DataSet ExecuteDataset

        /// <summary>
        /// ExecuteDataset
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public DataSet ExecuteDataset(string commandText)
        {
            return ExecuteDataset(CommandType.Text, commandText, null, new DataSet(), null);
        }

        /// <summary>
        /// ExecuteDataset
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public DataSet ExecuteDataset(CommandType commandType, string commandText)
        {
            return ExecuteDataset(commandType, commandText, null, new DataSet(), null);
        }

        /// <summary>
        /// ExecuteDataset
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public DataSet ExecuteDataset(string commandText, QueryParameterCollection commandParameters)
        {
            return ExecuteDataset(CommandType.Text, commandText, commandParameters, new DataSet(), null);
        }

        /// <summary>
        /// ExecuteDataset
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public DataSet ExecuteDataset(string commandText, DataSet ds)
        {
            return ExecuteDataset(CommandType.Text, commandText, null, ds, null);
        }

        /// <summary>
        /// ExecuteDataset
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataSet ExecuteDataset(string commandText, string tableName)
        {
            return ExecuteDataset(CommandType.Text, commandText, null, new DataSet(), tableName);
        }

        /// <summary>
        /// ExecuteDataset
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public DataSet ExecuteDataset(CommandType commandType, string commandText,
                                      QueryParameterCollection commandParameters)
        {
            return ExecuteDataset(commandType, commandText, commandParameters, new DataSet(), null);
        }

        /// <summary>
        /// ExecuteDataset
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public DataSet ExecuteDataset(CommandType commandType, string commandText, DataSet ds)
        {
            return ExecuteDataset(commandType, commandText, null, ds, null);
        }

        /// <summary>
        /// ExecuteDataset
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataSet ExecuteDataset(CommandType commandType, string commandText, string tableName)
        {
            return ExecuteDataset(commandType, commandText, null, new DataSet(), tableName);
        }

        /// <summary>
        /// ExecuteDataset
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public DataSet ExecuteDataset(string commandText, QueryParameterCollection commandParameters, DataSet ds)
        {
            return ExecuteDataset(CommandType.Text, commandText, commandParameters, ds, null);
        }

        /// <summary>
        /// ExecuteDataset
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataSet ExecuteDataset(string commandText, QueryParameterCollection commandParameters, string tableName)
        {
            return ExecuteDataset(CommandType.Text, commandText, commandParameters, new DataSet(), tableName);
        }

        /// <summary>
        /// ExecuteDataset
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="ds"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataSet ExecuteDataset(string commandText, DataSet ds, string tableName)
        {
            return ExecuteDataset(CommandType.Text, commandText, null, ds, tableName);
        }

        /// <summary>
        /// ExecuteDataset
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public DataSet ExecuteDataset(CommandType commandType, string commandText,
                                      QueryParameterCollection commandParameters, DataSet ds)
        {
            return ExecuteDataset(commandType, commandText, commandParameters, ds, null);
        }

        /// <summary>
        /// ExecuteDataset
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataSet ExecuteDataset(CommandType commandType, string commandText,
                                      QueryParameterCollection commandParameters, string tableName)
        {
            return ExecuteDataset(commandType, commandText, commandParameters, new DataSet(), tableName);
        }

        /// <summary>
        /// ExecuteDataset
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="ds"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataSet ExecuteDataset(CommandType commandType, string commandText, DataSet ds, string tableName)
        {
            return ExecuteDataset(commandType, commandText, null, ds, tableName);
        }

        /// <summary>
        /// ExecuteDataset
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <param name="ds"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataSet ExecuteDataset(string commandText, QueryParameterCollection commandParameters, DataSet ds,
                                      string tableName)
        {
            return ExecuteDataset(CommandType.Text, commandText, commandParameters, ds, tableName);
        }

        /// <summary>
        /// ExecuteDataset
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <param name="ds"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public abstract DataSet ExecuteDataset(CommandType commandType, string commandText,
                                               QueryParameterCollection commandParameters, DataSet ds, string tableName);

        #endregion

        #region ExecuteNonQuery

        /// <summary>
        /// /ExecuteNonQuery
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string commandText)
        {
            return ExecuteNonQuery(CommandType.Text, commandText, null);
        }

        /// <summary>
        /// ExecuteNonQuery
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(CommandType commandType, string commandText)
        {
            return ExecuteNonQuery(commandType, commandText, null);
        }

        /// <summary>
        /// ExecuteNonQuery
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string commandText, QueryParameterCollection commandParameters)
        {
            return ExecuteNonQuery(CommandType.Text, commandText, commandParameters);
        }

        /// <summary>
        /// ExecuteNonQuery
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public abstract int ExecuteNonQuery(CommandType commandType, string commandText,
                                            QueryParameterCollection commandParameters);

        #endregion

        #region ExecuteReader

        /// <summary>
        /// ExecuteReader
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public IDataReader ExecuteReader(string commandText)
        {
            return ExecuteReader(CommandType.Text, commandText, null);
        }

        /// <summary>
        /// ExecuteReader
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public IDataReader ExecuteReader(CommandType commandType, string commandText)
        {
            return ExecuteReader(commandType, commandText, null);
        }

        /// <summary>
        /// ExecuteReader
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public IDataReader ExecuteReader(string commandText, QueryParameterCollection commandParameters)
        {
            return ExecuteReader(CommandType.Text, commandText, commandParameters);
        }

        /// <summary>
        /// ExecuteReader
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public abstract IDataReader ExecuteReader(CommandType commandType, string commandText,
                                                  QueryParameterCollection commandParameters);

        #endregion

        #region ExecuteReader(Action<IDataReader> ...

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="commandText"></param>
        public void ExecuteReader(Action<IDataReader> action, string commandText)
        {
            ExecuteReader(action, CommandType.Text, commandText, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        public void ExecuteReader(Action<IDataReader> action, CommandType commandType, string commandText)
        {
            ExecuteReader(action, commandType, commandText, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        public void ExecuteReader(Action<IDataReader> action, string commandText,
                                  QueryParameterCollection commandParameters)
        {
            ExecuteReader(action, CommandType.Text, commandText, commandParameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        public void ExecuteReader(Action<IDataReader> action, CommandType commandType, string commandText,
                                  QueryParameterCollection commandParameters)
        {
            IDataReader reader = null;
            try
            {
                reader = ExecuteReader(commandType, commandText, commandParameters);

                action(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                Close();
            }
        }

        #endregion

        #region ExecuteScalar

        /// <summary>
        /// ExecuteScalar
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public object ExecuteScalar(string commandText)
        {
            return ExecuteScalar(CommandType.Text, commandText, null);
        }

        /// <summary>
        /// ExecuteScalar
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public object ExecuteScalar(CommandType commandType, string commandText)
        {
            return ExecuteScalar(commandType, commandText, null);
        }

        /// <summary>
        /// ExecuteScalar
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public object ExecuteScalar(string commandText, QueryParameterCollection commandParameters)
        {
            return ExecuteScalar(CommandType.Text, commandText, commandParameters);
        }

        /// <summary>
        /// ExecuteScalar
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public abstract object ExecuteScalar(CommandType commandType, string commandText,
                                             QueryParameterCollection commandParameters);

        #endregion

        #region XmlReader ExecuteXmlReader

        /// <summary>
        /// ExecuteXmlReader
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public XmlReader ExecuteXmlReader(string commandText)
        {
            return ExecuteXmlReader(CommandType.Text, commandText);
        }

        /// <summary>
        /// ExecuteXmlReader
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public XmlReader ExecuteXmlReader(CommandType commandType, string commandText)
        {
            return ExecuteXmlReader(commandType, commandText, null);
        }

        /// <summary>
        /// ExecuteXmlReader
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public XmlReader ExecuteXmlReader(string commandText, QueryParameterCollection commandParameters)
        {
            return ExecuteXmlReader(CommandType.Text, commandText, commandParameters);
        }

        /// <summary>
        /// ExecuteXmlReader
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public abstract XmlReader ExecuteXmlReader(CommandType commandType, string commandText,
                                                   QueryParameterCollection commandParameters);

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        public void Open()
        {
            if (DbConnection.State.Equals(ConnectionState.Closed))
            {
                DbConnection.Open();
            }
        }

        #endregion

        /// <summary>
        /// 同步参数
        /// </summary>
        /// <param name="commandParameters"></param>
        protected void SyncParameter(QueryParameterCollection commandParameters)
        {
            if ((commandParameters != null) && (commandParameters.Count > 0))
            {
                for (int i = 0; i < commandParameters.Count; i++)
                {
                    commandParameters[i].SyncParameter();
                }
            }
        }
    }
}