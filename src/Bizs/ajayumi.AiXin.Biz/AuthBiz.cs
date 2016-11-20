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

//***************************************************//
// 密码加密规则 = 加密方法(用户名+加密方法(明文密码))
//***************************************************//

namespace ajayumi.AiXin.Biz
{
    /// <summary>
    /// 认证业务
    /// </summary>
    public class AuthBiz : IAuthBiz
    {
        private readonly IUserDao m_Dao = null;
        private readonly CryptoProvider m_CryptoProvider = null;

        public AuthBiz(IUserDao dao, CryptoProvider crypto)
        {
            m_Dao = dao;
            m_CryptoProvider = crypto;
        }

        public ResultInfo<User> SignIn(string account, string pwd)
        {
            ResultInfo<User> result = new ResultInfo<User>();
            try
            {
                var user = this.m_Dao.FindByName(account);
                if (user == null)
                {
                    user = this.m_Dao.FindByEmail(account);
                    if (user == null)
                    {
                        user = this.m_Dao.FindByTelphone(account);
                    }
                }

                if (user == null)
                {
                    result.RCode = "1001";
                    result.RMsg = "用户不存在";
                    result.Success = false;
                    return result;
                }

                string cryptoPwd = this.m_CryptoProvider.Encrypt(user.Name + this.m_CryptoProvider.Encrypt(pwd));
                if (user.Password.Equals(cryptoPwd))
                {
                    result.RData = user;
                    return result;
                }
                else
                {
                    result.RCode = "1002";
                    result.RMsg = "用户名、密码有误";
                    result.Success = false;
                    return result;
                }
            }
            catch (Exception ex)
            {
                result = new ResultInfo<User>(ex);
            }

            return result;
        }

        public ResultInfo<User> SignUp(string name, string email, string pwd)
        {
            ResultInfo<User> result = new ResultInfo<User>();
            try
            {
                //为空判断
                if (string.IsNullOrEmpty(name))
                {
                    result.RCode = "1001";
                    result.RMsg = "用户名不能为空";
                    result.Success = false;
                    return result;
                }
                if (string.IsNullOrEmpty(email))
                {
                    result.RCode = "1002";
                    result.RMsg = "电子邮箱不能为空";
                    result.Success = false;
                    return result;
                }
                if (string.IsNullOrEmpty(pwd))
                {
                    result.RCode = "1003";
                    result.RMsg = "密码不能为空";
                    result.Success = false;
                    return result;
                }

                //唯一性判断
                User user = this.m_Dao.FindByName(name);
                if (user != null)
                {
                    result.RCode = "1004";
                    result.RMsg = "用户名已经存在";
                    result.Success = false;
                    return result;
                }
                user = this.m_Dao.FindByEmail(email);
                if (user != null)
                {
                    result.RCode = "1005";
                    result.RMsg = "电子邮箱已经存在";
                    result.Success = false;
                    return result;
                }

                string cryptoPwd = this.m_CryptoProvider.Encrypt(name + this.m_CryptoProvider.Encrypt(pwd));
                user = this.m_Dao.Add(new User() { Name = name, Email = email, Password = cryptoPwd });

                result.RMsg = "注册成功";
                result.RData = user;
            }
            catch (Exception ex)
            {
                result = new ResultInfo<User>(ex);
            }

            return result;
        }

        public ResultInfo SaveStatus(UserLogin model)
        {
            ResultInfo result = null;
            try
            {
                this.m_Dao.AddUserLogin(model);

                result = new ResultInfo() { RMsg = "更新用户状态成功" };
            }
            catch (Exception ex)
            {
                result = new ResultInfo(ex);
            }
            return result;
        }

    }
}
