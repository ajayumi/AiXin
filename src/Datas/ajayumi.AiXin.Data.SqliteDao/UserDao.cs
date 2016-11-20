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
    public class UserDao : DaoBase<User>, IUserDao
    {
        private const string BASE_SQL_STR = "select *,(select t1.Name from tb_Grade t1 where t1.Score < t.Score limit 1) GradeName from tb_user t";

        public User Add(User model)
        {
            try
            {
                string sql = "insert into tb_user(Name, Email, Password, CreationDTime) values(@Name, @Email, @Password, @CreationDTime); SELECT last_insert_rowid();";
                long id = this.ExecuteScalar<long>(sql, new
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password,
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

        public void AddUserLogin(UserLogin model)
        {
            try
            {
                string sql = "insert into tb_UserLogin(UserId, CustomId, IP, ConnectState, CreationDTime) values(@UserId, @CustomId, @IP, @ConnectState, @CreationDTime);";
                long id = this.ExecuteScalar<long>(sql, new
                {
                    UserId = model.UserId,
                    CustomId = model.CustomId,
                    IP = model.IP,
                    ConnectState = model.ConnectState,
                    CreationDTime = DateTime.Now,
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ChangePwd(long id, string pwd)
        {
            try
            {
                string sql = "update tb_user set Password = @Password Where Id = @Id;";

                var obj = new
                {
                    Id = id,
                    Password = pwd
                };
                this.ExecuteNonQuery(sql, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string name)
        {
            throw new NotImplementedException();
        }

        public User FindByEmail(string email)
        {
            User user = null;
            try
            {
                string sql = $"{BASE_SQL_STR} where t.Email = @Email;";
                using (SafeDataReader dr = new SafeDataReader(this.ExecuteReader(sql, new { Email = email })))
                {
                    while (dr.Read())
                    {
                        user = this.ReaderToModel(dr);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return user;
        }

        public User FindById(long id)
        {
            User user = null;
            try
            {
                string sql = $"{BASE_SQL_STR} where t.Id = @Id;";
                using (SafeDataReader dr = new SafeDataReader(this.ExecuteReader(sql, new { Id = id })))
                {
                    while (dr.Read())
                    {
                        user = this.ReaderToModel(dr);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return user;
        }

        public User FindByName(string name)
        {
            User user = null;
            try
            {
                string sql = $"{BASE_SQL_STR} where t.Name = @Name;";
                using (SafeDataReader dr = new SafeDataReader(this.ExecuteReader(sql, new { Name = name })))
                {
                    while (dr.Read())
                    {
                        user = this.ReaderToModel(dr);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return user;
        }

        public User FindByTelphone(string telphone)
        {
            User user = null;
            try
            {
                string sql = $"{BASE_SQL_STR} where t.Telphone = @Telphone;";
                using (SafeDataReader dr = new SafeDataReader(this.ExecuteReader(sql, new { Telphone = telphone })))
                {
                    while (dr.Read())
                    {
                        user = this.ReaderToModel(dr);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return user;
        }

        public IEnumerable<User> GetDataPager(SqlCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetList()
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

        public void Update(User model)
        {
            try
            {
                string sql = "update tb_user set Email = @Email, Telphone = @Telphone, NickName = @NickName, " +
                    "RealName = @RealName, Avatar = @Avatar, Gender = @Gender, Birthday = @Birthday, QQ = @QQ, " +
                    "WeChat = @WeChat, Address = @Address, Hobby = @Hobby, Signature = @Signature, " +
                    "Remark = @Remark Where Id = @Id;";

                var obj = new
                {
                    Id = model.Id,
                    //Name = model.Name,
                    Email = model.Email,
                    Telphone = model.Telphone,
                    //Password = model.Password,
                    NickName = model.NickName,
                    RealName = model.RealName,
                    Avatar = model.Avatar,
                    Gender = model.Gender,
                    Birthday = model.Birthday,
                    QQ = model.QQ,
                    WeChat = model.WeChat,
                    Address = model.Address,
                    Hobby = model.Hobby,
                    Signature = model.Signature,
                    //Score = model.UserGrade.Score,
                    //CreationDTime = model.CreationDTime,
                    Remark = model.Remark
                };
                this.ExecuteNonQuery(sql, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateAvatar(long id, string avatar)
        {
            try
            {
                string sql = "update tb_user set Avatar = @Avatar Where Id = @Id;";

                var obj = new
                {
                    Id = id,
                    Avatar = avatar
                };
                this.ExecuteNonQuery(sql, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateStatus(User user)
        {
            //try
            //{
            //    string sql = $"update tb_User set CustomId = @CustomId where Id = @Id;";
            //    this.ExecuteNonQuery(sql, new { CustomId = user.CustomId, Id = user.Id });
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        protected override User ReaderToModel(SafeDataReader dr)
        {
            User user = new User();
            user.Address = dr.GetString(nameof(User.Address));
            user.Avatar = dr.GetString(nameof(User.Avatar));
            user.Birthday = dr.GetDateTime(nameof(User.Birthday));
            user.CreationDTime = dr.GetDateTime(nameof(User.CreationDTime));
            //user.CustomId = dr.GetString(nameof(User.CustomId));
            user.Email = dr.GetString(nameof(User.Email));
            user.Gender = dr.GetInt16(nameof(User.Gender)) == 0 ? EGender.Male : EGender.Female;
            user.Hobby = dr.GetString(nameof(User.Hobby));
            user.Id = dr.GetInt64(nameof(User.Id));
            user.Name = dr.GetString(nameof(User.Name));
            user.NickName = dr.GetString(nameof(User.NickName));
            user.Password = dr.GetString(nameof(User.Password));
            user.QQ = dr.GetString(nameof(User.QQ));
            user.RealName = dr.GetString(nameof(User.RealName));
            user.Remark = dr.GetString(nameof(User.Remark));
            user.Signature = dr.GetString(nameof(User.Signature));
            user.Telphone = dr.GetString(nameof(User.Telphone));
            user.WeChat = dr.GetString(nameof(User.WeChat));

            string gradeName = dr.GetString("GradeName");
            if (string.IsNullOrEmpty(gradeName))
            {
                gradeName = "贱民";
            }
            Grade grade = new Grade() { Name = gradeName, Score = dr.GetInt64(nameof(Grade.Score)) };
            user.UserGrade = grade;

            return user;
        }
    }
}
