#region Copyright
//************************************************************************************/
//	创建人员(Creator)    ：ajayumi
//	创建日期(Date)       ：2016-11-20
//  联系方式(Email)      ：aj-ayumi@163.com; gajayumi@gmail.com;
//  描    述(Description)：
//
//	Copyright(C) 2009-2016 ajayumi.All rights reserved.
//************************************************************************************/
#endregion

using Dapper;
using System.Configuration;
using System.Data;

namespace ajayumi.AiXin.Data
{
    public abstract class DaoBase<TModel>
    {
        private const int DEFAULT_CMD_TIMEOUT = 90;

        protected IDbConnection Connector
        {
            get;
            private set;
        }

        protected string ConnectionString
        {
            get;
            private set;
        }

        protected DaoBase()
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            ConnectionString = connStr;
            Connector = CreateConnection(connStr);
        }

        protected abstract IDbConnection CreateConnection(string connStr);

        /// <summary>
        /// 数据阅读器中得到实体对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <returns></returns>
        protected abstract TModel ReaderToModel(SafeDataReader dr);

        protected TResult ExecuteScalar<TResult>(string sql, object param, int cmdTimeout = DEFAULT_CMD_TIMEOUT)
        {
            return Connector.ExecuteScalar<TResult>(sql, param: param, commandTimeout: cmdTimeout);
        }

        protected IDataReader ExecuteReader(string sql, object param, int cmdTimeout = DEFAULT_CMD_TIMEOUT)
        {
            return Connector.ExecuteReader(sql, param: param, commandTimeout: cmdTimeout);
        }

        protected int ExecuteNonQuery(string sql, object param, int cmdTimeout = DEFAULT_CMD_TIMEOUT)
        {
            return Connector.Execute(sql, param: param, commandTimeout: cmdTimeout);
        }

    }
}
