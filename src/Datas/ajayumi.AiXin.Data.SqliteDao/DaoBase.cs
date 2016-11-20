﻿#region Copyright
//************************************************************************************/
//	创建人员(Creator)    ：ajayumi
//	创建日期(Date)       ：2016-11-20
//  联系方式(Email)      ：aj-ayumi@163.com; gajayumi@gmail.com;
//  描    述(Description)：
//
//	Copyright(C) 2009-2016 ajayumi.All rights reserved.
//************************************************************************************/
#endregion

using System.Data;
using System.Data.SQLite;
using System.Threading;

namespace ajayumi.AiXin.Data.SqliteDao
{
    public abstract class DaoBase<TModel> : ajayumi.AiXin.Data.DaoBase<TModel>
    {
        protected readonly ReaderWriterLockSlim m_ReaderWriterLock = new ReaderWriterLockSlim();

        protected DaoBase()
        {
        }

        protected override IDbConnection CreateConnection(string connStr)
        {
            return new SQLiteConnection(connStr);
        }
    }
}
