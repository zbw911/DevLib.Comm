// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：QueryParameter.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System;
using System.Data;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Data.SqlClient;

namespace Dev.DBUtility
{
    /// <summary>
    /// 用于查询的参数
    /// </summary>
    public sealed class QueryParameter : MarshalByRefObject, IDbDataParameter, IDataParameter, ICloneable
    {
        private DbType _dbType;
        private ParameterDirection _direction;
        private bool _forceSize;
        private string _name;
        private int _offset;
        private byte _precision;
        private IDbDataParameter _realParameter;
        private byte _scale;
        private int _size;
        private string _sourceColumn;
        private bool _suppress;
        private object _value;
        private DataRowVersion _version;

        /// <summary>
        /// 初始化参数
        /// </summary>
        public QueryParameter()
        {
            _value = null;
            _direction = ParameterDirection.Input;
            _size = -1;
            _version = DataRowVersion.Current;
            _forceSize = false;
            _offset = 0;
            _suppress = false;
        }

        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="dbType"></param>
        public QueryParameter(string parameterName, DbType dbType)
            : this()
        {
            _name = parameterName;
            _dbType = dbType;
        }

        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="Value"></param>
        public QueryParameter(string parameterName, object Value)
            : this()
        {
            _name = parameterName;
            _value = Value;
        }

        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="dbType"></param>
        /// <param name="size"></param>
        public QueryParameter(string parameterName, DbType dbType, int size)
            : this()
        {
            _name = parameterName;
            _dbType = dbType;
            _size = size;
        }

        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="dbType"></param>
        /// <param name="size"></param>
        /// <param name="Value"></param>
        public QueryParameter(string parameterName, DbType dbType, int size, object Value)
            : this()
        {
            _name = parameterName;
            _dbType = dbType;
            _size = size;
            _value = Value;
        }

        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="Value"></param>
        /// <param name="dbType"></param>
        public QueryParameter(string parameterName, object Value, DbType dbType)
            : this()
        {
            _name = parameterName;
            _dbType = dbType;
            _value = Value;
        }

        /// <summary>
        ///  包含方向的方法
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="dbType"></param>
        /// <param name="Value"></param>
        /// <param name="direction"></param>
        public QueryParameter(string parameterName, DbType dbType, ParameterDirection direction, object Value)
            : this()
        {
            _name = parameterName;
            _dbType = dbType;
            _value = Value;
            _direction = direction;
        }

        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="dbType"></param>
        /// <param name="size"></param>
        /// <param name="sourceColumn"></param>
        public QueryParameter(string parameterName, DbType dbType, int size, string sourceColumn)
            : this()
        {
            _name = parameterName;
            _dbType = dbType;
            _size = size;
            _sourceColumn = sourceColumn;
        }


        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="dbType"></param>
        /// <param name="size"></param>
        /// <param name="direction"></param>
        /// <param name="isNullable"></param>
        /// <param name="precision"></param>
        /// <param name="scale"></param>
        /// <param name="sourceColumn"></param>
        /// <param name="sourceVersion"></param>
        /// <param name="Value"></param>
        public QueryParameter(string parameterName, DbType dbType, int size, ParameterDirection direction,
                              bool isNullable, byte precision, byte scale, string sourceColumn,
                              DataRowVersion sourceVersion, object Value)
            : this()
        {
            _name = parameterName;
            _dbType = dbType;
            _size = size;
            _direction = direction;
            IsNullable = isNullable;
            _precision = precision;
            _scale = scale;
            _sourceColumn = sourceColumn;
            _version = sourceVersion;
            _value = Value;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Offset
        {
            get { return _offset; }
            set { _offset = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public IDbDataParameter RealParameter
        {
            get { return _realParameter; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Suppress
        {
            get { return _suppress; }
            set { _suppress = value; }
        }

        #region ICloneable Members

        /// <summary>
        /// 参数克隆
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            var parameter = new QueryParameter();
            parameter.SetProperties(_name, _sourceColumn, _version, _precision, _scale, _size, _forceSize, _offset,
                                    _direction, _value, _dbType, _suppress);
            return parameter;
        }

        #endregion

        #region IDbDataParameter Members

        /// <summary>
        /// 
        /// </summary>
        public DbType DbType
        {
            get { return _dbType; }
            set { _dbType = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public ParameterDirection Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        public bool IsNullable { get; set; }

        /// <summary>
        /// 参数名
        /// </summary>
        public string ParameterName
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public byte Precision
        {
            get { return _precision; }
            set { _precision = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public byte Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Size
        {
            get
            {
                if (_forceSize)
                {
                    return _size;
                }
                return 0;
            }
            set
            {
                if (value < 0)
                {
                    throw new Exception(value.ToString());
                }
                if (value != 0)
                {
                    _forceSize = true;
                    _size = value;
                }
                else
                {
                    _forceSize = false;
                    _size = -1;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string SourceColumn
        {
            get
            {
                if (_sourceColumn == null)
                {
                    return string.Empty;
                }
                return _sourceColumn;
            }
            set { _sourceColumn = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DataRowVersion SourceVersion
        {
            get { return _version; }
            set { _version = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public object Value
        {
            get
            {
                if (Equals(_value, null))
                {
                    return DBNull.Value;
                }
                return _value;
            }
            set { _value = value; }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseType"></param>
        public void InitRealParameter(DatabaseType databaseType)
        {
            if (Equals(_realParameter, null))
            {
                switch (databaseType)
                {
                    case DatabaseType.MSSQLServer:
                        _realParameter = new SqlParameter();
                        break;

                    case DatabaseType.Oracle:
                        _realParameter = new OracleParameter();
                        break;

                    case DatabaseType.OleDBSupported:
                        _realParameter = new OleDbParameter();
                        break;
                }
            }
            RealParameter.DbType = DbType;
            RealParameter.Direction = Direction;
            RealParameter.ParameterName = ParameterName;
            RealParameter.Precision = Precision;
            RealParameter.Scale = Scale;
            RealParameter.Size = Size;
            RealParameter.SourceColumn = SourceColumn;
            RealParameter.SourceVersion = SourceVersion;
            RealParameter.Value = Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="column"></param>
        /// <param name="version"></param>
        /// <param name="precision"></param>
        /// <param name="scale"></param>
        /// <param name="size"></param>
        /// <param name="forceSize"></param>
        /// <param name="offset"></param>
        /// <param name="direction"></param>
        /// <param name="Value"></param>
        /// <param name="type"></param>
        /// <param name="suppress"></param>
        public void SetProperties(string name, string column, DataRowVersion version, byte precision, byte scale,
                                  int size, bool forceSize, int offset, ParameterDirection direction, object Value,
                                  DbType type, bool suppress)
        {
            ParameterName = name;
            _sourceColumn = column;
            SourceVersion = version;
            Precision = precision;
            _scale = scale;
            _size = size;
            _forceSize = forceSize;
            _offset = offset;
            Direction = direction;
            if (Value is ICloneable)
            {
                Value = ((ICloneable) Value).Clone();
            }
            _value = Value;
            Suppress = suppress;
        }

        /// <summary>
        /// 
        /// </summary>
        public void SyncParameter()
        {
            if (!Equals(_realParameter, null))
            {
                SetProperties(RealParameter.ParameterName, RealParameter.SourceColumn, RealParameter.SourceVersion,
                              RealParameter.Precision, RealParameter.Scale, RealParameter.Size, _forceSize, _offset,
                              RealParameter.Direction, RealParameter.Value, RealParameter.DbType, Suppress);
            }
        }

        /// <summary>
        /// 参数名
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ParameterName;
        }
    }
}