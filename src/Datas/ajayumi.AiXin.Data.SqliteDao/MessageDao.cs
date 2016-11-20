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

using ajayumi.AiXin.Data.IDao;
using ajayumi.AiXin.Model;
using System;
using System.Collections.Generic;

namespace ajayumi.AiXin.Data.SqliteDao
{
    public class MessageDao : DaoBase<Message>, IMessageDao
    {
        public Message Add(Message model)
        {
            try
            {
                string sql = "insert into tb_Message(FromUserId, ToUserId, Content, CreationDTime) values(@FromUserId, @ToUserId, @Content, @CreationDTime); SELECT last_insert_rowid();";
                long id = this.ExecuteScalar<long>(sql, new
                {
                    FromUserId = model.FromUserId,
                    ToUserId = model.ToUserId,
                    Content = model.Content,
                    CreationDTime = DateTime.Now,
                });
                model.Id = id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return model;
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string name)
        {
            throw new NotImplementedException();
        }

        public Message FindById(long id)
        {
            Message result = null;
            try
            {
                string sql = "select * from tb_Message t where t.Id = @Id;";
                using (SafeDataReader dr = new SafeDataReader(this.ExecuteReader(sql, new { Id = id })))
                {
                    while (dr.Read())
                    {
                        result = this.ReaderToModel(dr);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public IEnumerable<Message> GetDataPager(SqlCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Message> GetList()
        {
            throw new NotImplementedException();
        }

        public long GetRecordCount()
        {
            throw new NotImplementedException();
        }

        public long GetRecordCount(SqlCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public void Update(Message model)
        {
            throw new NotImplementedException();
        }

        protected override Message ReaderToModel(SafeDataReader dr)
        {
            Message msg = new Message() { Content = dr.GetString(nameof(Message.Content)), CreationDTime = dr.GetDateTime(nameof(Message.CreationDTime)) };
            long fromUserId = dr.GetInt64(nameof(Message.FromUserId));
            long toUserId = dr.GetInt64(nameof(Message.ToUserId));

            UserDao dao = new UserDao();
            msg.FromUser = dao.FindById(fromUserId);
            msg.ToUser = dao.FindById(toUserId);

            return msg;
        }
    }
}
