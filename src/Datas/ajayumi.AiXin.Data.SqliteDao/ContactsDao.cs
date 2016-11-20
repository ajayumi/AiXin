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
    public class ContactsDao : DaoBase<Contacts>, IContactsDao
    {
        public Contacts Add(Contacts model)
        {
            try
            {
                string sql = "insert into tb_Contacts(OUserId, CUserId, CustomName, CreationDTime) values(@OUserId, @CUserId, @CustomName, @CreationDTime); SELECT last_insert_rowid();";
                long id = this.ExecuteScalar<long>(sql, new
                {
                    OUserId = model.OUser.Id,
                    CUserId = model.CUser.Id,
                    CustomName = model.CustomName,
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

        public bool Exists(long oUserId, long cUserId)
        {
            bool result = false;
            try
            {
                string sql = "select count(0) from tb_Contacts t where t.OUserId = @OUserId and t.CUserId = @CUserId;";
                int count = this.ExecuteScalar<int>(sql, new { OUserId = oUserId, CUserId = cUserId });
                result = count > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public Contacts FindById(long id)
        {
            throw new NotImplementedException();
        }

        public Contacts FindById(long oUserId, long cUserId)
        {
            Contacts result = null;
            try
            {
                string sql = "select * from tb_Contacts t where t.OUserId = @OUserId and t.CUserId = @CUserId;";
                using (SafeDataReader dr = new SafeDataReader(this.ExecuteReader(sql, new { OUserId = oUserId, CUserId = cUserId })))
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

        public IEnumerable<Contacts> GetDataPager(SqlCriteria criteria)
        {
            List<Contacts> lst = new List<Contacts>();
            try
            {
                using (SafeDataReader dr = new SafeDataReader(this.ExecuteReader(criteria.GenSqlScript(), criteria.GenSqlParams())))
                {
                    while (dr.Read())
                    {
                        lst.Add(this.ReaderToModel(dr));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }

        public IEnumerable<Contacts> GetList()
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

        public void Update(Contacts model)
        {
            try
            {
                string sql = "update tb_Contacts set CustomName = @CustomName where OUserId = @OUserId and CUserId = @CUserId;";
                this.ExecuteNonQuery(sql, new
                {
                    OUserId = model.OUser.Id,
                    CUserId = model.CUser.Id,
                    CustomName = model.CustomName
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override Contacts ReaderToModel(SafeDataReader dr)
        {
            Contacts contacts = new Contacts() { CustomName = dr.GetString(nameof(Contacts.CustomName)), CreationDTime = dr.GetDateTime(nameof(Contacts.CreationDTime)) };
            long oUserId = dr.GetInt64("OUserId");
            long cUserId = dr.GetInt64("CUserId");

            UserDao dao = new UserDao();
            contacts.OUser = dao.FindById(oUserId);
            contacts.CUser = dao.FindById(cUserId);

            return contacts;
        }
    }
}
