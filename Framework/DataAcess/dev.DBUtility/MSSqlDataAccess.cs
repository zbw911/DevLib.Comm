// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：MSSqlDataAccess.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using Dev.Log;

namespace Dev.DBUtility
{
    /// <summary>
    /// SQL SERVER 数据库访问类
    /// </summary>
    public sealed class MSSqlDataAccess : AbstractDataAccess
    {
        //private SqlConnection m_DbConnection;
        //private SqlTransaction trans;

        /// <summary>
        /// 通过连接进行构造
        /// </summary>
        /// <param name="conn"></param>
        public MSSqlDataAccess(SqlConnection conn)
        {
            trans = null;
            m_DbConnection = conn;
        }

        /// <summary>
        /// 通过数据库连接串进行构造的方法
        /// </summary>
        /// <param name="connectionString"></param>
        public MSSqlDataAccess(string connectionString)
        {
            trans = null;
            m_DbConnection = new SqlConnection(connectionString);
        }

        /// <summary>
        /// 数据库的类型
        /// </summary>
        public override DatabaseType DatabaseType
        {
            get { return DatabaseType.MSSQLServer; }
        }

        /// <summary>
        /// 连接
        /// </summary>
        public override IDbConnection DbConnection
        {
            get { return m_DbConnection; }
        }


        /// <summary>
        /// 返回数据集,要从外部进行数据集 ( ds = new dataset()) 的传递
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
                var cmd = new SqlCommand();
                PrepareCommand(cmd, commandType, commandText, commandParameters);
                var adapter = new SqlDataAdapter(cmd);
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
                exceptioned = true;
                throw;
            }
            finally
            {
                Close();
            }
        }

        /// <summary>
        /// ExecuteNonQuery
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
                var cmd = new SqlCommand();
                PrepareCommand(cmd, commandType, commandText, commandParameters);
                int num = cmd.ExecuteNonQuery();
                base.SyncParameter(commandParameters);
                cmd.Parameters.Clear();
                return num;
            }
            catch
            {
                exceptioned = true;
                throw;
            }
            finally
            {
                Close();
            }
        }

        /// <summary>
        /// READER
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public override IDataReader ExecuteReader(CommandType commandType, string commandText,
                                                  QueryParameterCollection commandParameters)
        {
            var cmd = new SqlCommand();
            PrepareCommand(cmd, commandType, commandText, commandParameters);
            SqlDataReader reader = cmd.ExecuteReader();
            base.SyncParameter(commandParameters);
            cmd.Parameters.Clear();
            return reader;
        }


        /// <summary>
        /// ExecuteScalar
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
                var cmd = new SqlCommand();
                PrepareCommand(cmd, commandType, commandText, commandParameters);
                object obj2 = cmd.ExecuteScalar();
                base.SyncParameter(commandParameters);
                cmd.Parameters.Clear();
                return obj2;
            }
            catch
            {
                //if (trans != null)
                //{
                //    RollbackTransaction();
                //}

                exceptioned = true;
                throw;
            }
            finally
            {
                Close();
            }
        }

        /// <summary>
        /// ExecuteXmlReader
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public override XmlReader ExecuteXmlReader(CommandType commandType, string commandText,
                                                   QueryParameterCollection commandParameters)
        {
            try
            {
                var cmd = new SqlCommand();
                PrepareCommand(cmd, commandType, commandText, commandParameters);
                XmlReader reader = cmd.ExecuteXmlReader();
                base.SyncParameter(commandParameters);
                cmd.Parameters.Clear();
                return reader;
            }
            catch
            {
                //if (trans != null)
                //{
                //    RollbackTransaction();
                //}

                exceptioned = true;
                throw;
            }
            finally
            {
                Close();
            }
        }


        /// <summary>
        /// 参数准备
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        private void PrepareCommand(SqlCommand cmd, CommandType commandType, string commandText,
                                    QueryParameterCollection commandParameters)
        {
            cmd.CommandType = commandType;
            cmd.CommandText = commandText;
            cmd.Connection = (SqlConnection)m_DbConnection;
            cmd.Transaction = (SqlTransaction)trans;


            if (commandText.IndexOf("''") >= 0)
            {
                var blankname = "@____blankchar_______";
                commandText = commandText.Replace("''", blankname);

                if (commandParameters == null) commandParameters = new QueryParameterCollection();

                commandParameters.Add(blankname, "");
            }


            if ((commandParameters != null) && (commandParameters.Count > 0))
            {
                for (int i = 0; i < commandParameters.Count; i++)
                {
                    commandParameters[i].InitRealParameter(DatabaseType.MSSQLServer);
                    cmd.Parameters.Add(commandParameters[i].RealParameter as SqlParameter);
                }
            }

            Open();

            //if (this.checkSQL)
            //{
            string notallow = string.Empty;
            //所有的执行方式都检查参数，包括存储过程和SQL语句
            //if (CheckSQL.CheckSQLText(commandParameters, out notallow) <= 0) //语句中有不允许的内容，如果是开发人员写的，请改写sql语句或写存储过程实现
            //{
            //    Dev.Log.Loger.Error("sql语句中不允许出现：" + notallow);

            //    throw new Exception("数据格式不正确！！！");
            //}
            //}

            //if (this.checkParms)
            //{
            //string notallow;
            if (!commandType.Equals(CommandType.StoredProcedure) && CheckSQL.CheckSQLText(commandText, out notallow) <= 0)
            //语句中有不允许的内容，如果是开发人员写的，请改写sql语句或写存储过程实现
            {
                Loger.Error("sql语句中不允许出现：" + notallow);

                throw new Exception("数据格式不正确！！！注入字符" + notallow);
            }
            //}


            //DBLog.AddLog("", commandType, commandText, commandParameters);
        }
    }
}