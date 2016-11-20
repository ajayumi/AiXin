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
using ajayumi.AiXin.IBiz;
using ajayumi.AiXin.Infrastructure;
using ajayumi.AiXin.Model;
using ajayumi.Platform.Core.Security;
using System;
using System.Collections.Generic;

namespace ajayumi.AiXin.Biz
{
    public class UserBiz : IUserBiz
    {
        private readonly IUserDao m_Dao = null;
        private readonly CryptoProvider m_CryptoProvider = null;

        public UserBiz(IUserDao dao, CryptoProvider crypto)
        {
            m_Dao = dao;
            m_CryptoProvider = crypto;
        }

        public ResultInfo<IEnumerable<User>> SearchUser(string account)
        {
            ResultInfo<IEnumerable<User>> result = null;
            try
            {
                User user = this.m_Dao.FindByName(account);
                if (user == null)
                {
                    user = this.m_Dao.FindByEmail(account);
                    if (user == null)
                    {
                        user = this.m_Dao.FindByTelphone(account);
                    }
                }
                if (user != null)
                {
                    result = new ResultInfo<IEnumerable<User>>() { RMsg = "搜索用户成功", RData = new List<User> { user } };
                }
                else
                {
                    result = new ResultInfo<IEnumerable<User>>() { RCode = "1001", RMsg = "搜索用户不存在", Success = false };
                }
            }
            catch (Exception ex)
            {
                result = new ResultInfo<IEnumerable<User>>(ex);
            }
            return result;
        }

        public ResultInfo UpdateUser(User user)
        {
            ResultInfo result = null;
            try
            {
                this.m_Dao.Update(user);
                result = new ResultInfo() { RMsg = "修改用户成功" };
            }
            catch (Exception ex)
            {
                result = new ResultInfo(ex);
            }
            return result;
        }

        public ResultInfo<User> FindById(long id)
        {
            ResultInfo<User> result = null;
            try
            {
                User user = this.m_Dao.FindById(id);

                if (user != null)
                {
                    result = new ResultInfo<User>() {  RMsg = "查找用户成功", RData = user };
                }
                else
                {
                    result = new ResultInfo<User>() { RCode = "1001", RMsg = "查找用户不存在", Success = false };
                }
            }
            catch (Exception ex)
            {
                result = new ResultInfo<User>(ex);
            }
            return result;
        }

        public ResultInfo UpdateUserAvatar(long id, string avatar)
        {
            ResultInfo result = null;
            try
            {
                this.m_Dao.UpdateAvatar(id, avatar);
                result = new ResultInfo() {  RMsg = "修改用户头像成功" };
            }
            catch (Exception ex)
            {
                result = new ResultInfo(ex);
            }
            return result;
        }
    }
}
