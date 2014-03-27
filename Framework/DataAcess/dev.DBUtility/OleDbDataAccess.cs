// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：OleDbDataAccess.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Xml;

namespace Dev.DBUtility
{
    /// <summary>
    /// OLE 数据源访问 
    /// </summary>
    public sealed class OleDbDataAccess : AbstractDataAccess
    {
        //private OleDbConnection m_DbConnection;
        //private OleDbTransaction trans;
        /// <summary>
        /// OleDbDataAccess
        /// </summary>
        /// <param name="conn"></param>
        public OleDbDataAccess(OleDbConnection conn)
        {
            trans = null;
            m_DbConnection = conn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public OleDbDataAccess(string connectionString)
        {
            trans = null;
            m_DbConnection = new OleDbConnection(connectionString);
        }

        /// <summary>
        /// 
        /// </summary>
        public override DatabaseType DatabaseType
        {
            get { return DatabaseType.OleDBSupported; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override IDbConnection DbConnection
        {
            get { return m_DbConnection; }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <param name="ds"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public override DataSet ExecuteDataset(CommandType commandType, string commandText,
                                               QueryParameterCollection commandParameters, DataSet ds, string tableName)
        {
            try
            {
                var cmd = new OleDbCommand();
                PrepareCommand(cmd, commandType, commandText, commandParameters);
                var adapter = new OleDbDataAdapter(cmd);
                if (Equals(tableName, null) || (tableName.Length < 1))
                {
                    adapter.Fill(ds);
                }
                else
                {
                    adapter.Fill(ds, tableName);
                }
                base.SyncParameter(commandParameters);
                cmd.Parameters.Clear();
                return ds;
            }
            catch
            {
                if (trans != null)
                {
                    RollbackTransaction();
                }
                throw;
            }
            finally
            {
                Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public override int ExecuteNonQuery(CommandType commandType, string commandText,
                                            QueryParameterCollection commandParameters)
        {
            try
            {
                var cmd = new OleDbCommand();
                PrepareCommand(cmd, commandType, commandText, commandParameters);
                int num = cmd.ExecuteNonQuery();
                base.SyncParameter(commandParameters);
                cmd.Parameters.Clear();
                return num;
            }
            catch
            {
                if (trans != null)
                {
                    RollbackTransaction();
                }
                throw;
            }
            finally
            {
                Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public override IDataReader ExecuteReader(CommandType commandType, string commandText,
                                                  QueryParameterCollection commandParameters)
        {
            var cmd = new OleDbCommand();
            PrepareCommand(cmd, commandType, commandText, commandParameters);
            OleDbDataReader reader = cmd.ExecuteReader();
            base.SyncParameter(commandParameters);
            cmd.Parameters.Clear();
            return reader;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public override object ExecuteScalar(CommandType commandType, string commandText,
                                             QueryParameterCollection commandParameters)
        {
            try
            {
                var cmd = new OleDbCommand();
                PrepareCommand(cmd, commandType, commandText, commandParameters);
                object obj2 = cmd.ExecuteScalar();
                base.SyncParameter(commandParameters);
                cmd.Parameters.Clear();
                return obj2;
            }
            catch
            {
                if (trans != null)
                {
                    RollbackTransaction();
                }
                throw;
            }
            finally
            {
                Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public override XmlReader ExecuteXmlReader(CommandType commandType, string commandText,
                                                   QueryParameterCollection commandParameters)
        {
            XmlReader reader2;
            DataSet set = base.ExecuteDataset(commandType, commandText);
            base.SyncParameter(commandParameters);
            var input = new StringReader(set.GetXml());
            try
            {
                reader2 = new XmlTextReader(input);
            }
            catch (Exception exception)
            {
                input.Close();
                throw exception;
            }
            return reader2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        private void PrepareCommand(OleDbCommand cmd, CommandType commandType, string commandText,
                                    QueryParameterCollection commandParameters)
        {
            cmd.CommandType = commandType;
            cmd.CommandText = commandText;
            cmd.Connection = (OleDbConnection) m_DbConnection;
            cmd.Transaction = (OleDbTransaction) trans;
            if ((commandParameters != null) && (commandParameters.Count > 0))
            {
                for (int i = 0; i < commandParameters.Count; i++)
                {
                    commandParameters[i].InitRealParameter(DatabaseType.OleDBSupported);
                    cmd.Parameters.Add(commandParameters[i].RealParameter as OleDbParameter);
                }
            }

            Open();
        }
    }
}