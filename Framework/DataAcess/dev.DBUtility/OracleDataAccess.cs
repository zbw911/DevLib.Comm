// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：OracleDataAccess.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using System.Xml;

namespace Dev.DBUtility
{
    /// <summary>
    /// OracleDataAccess
    /// </summary>
    public sealed class OracleDataAccess : AbstractDataAccess
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        public OracleDataAccess(OracleConnection conn)
        {
            trans = null;
            m_DbConnection = conn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public OracleDataAccess(string connectionString)
        {
            trans = null;
            m_DbConnection = new OracleConnection(connectionString);
        }

        /// <summary>
        /// 
        /// </summary>
        public override DatabaseType DatabaseType
        {
            get { return DatabaseType.Oracle; }
        }

        /// <summary>
        /// ���ݿ�����
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
                var cmd = new OracleCommand();
                PrepareCommand(cmd, commandType, commandText, commandParameters);
                var adapter = new OracleDataAdapter(cmd);
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
                var cmd = new OracleCommand();
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
            var cmd = new OracleCommand();
            PrepareCommand(cmd, commandType, commandText, commandParameters);
            OracleDataReader reader = cmd.ExecuteReader();
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
                var cmd = new OracleCommand();
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
        private void PrepareCommand(OracleCommand cmd, CommandType commandType, string commandText,
                                    QueryParameterCollection commandParameters)
        {
            cmd.CommandType = commandType;
            cmd.CommandText = commandText;
            cmd.Connection = (OracleConnection) m_DbConnection;
            cmd.Transaction = (OracleTransaction) trans;
            if ((commandParameters != null) && (commandParameters.Count > 0))
            {
                for (int i = 0; i < commandParameters.Count; i++)
                {
                    commandParameters[i].InitRealParameter(DatabaseType.Oracle);
                    cmd.Parameters.Add(commandParameters[i].RealParameter as OracleParameter);
                }
            }

            Open();
        }
    }
}