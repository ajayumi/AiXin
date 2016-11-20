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

using ajayumi.AiXin.Infrastructure;
using ajayumi.AiXin.Model;

namespace ajayumi.AiXin.IBiz
{
    public interface IAuthBiz
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        ResultInfo<User> SignIn(string account, string pwd);

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        ResultInfo<User> SignUp(string name, string email, string pwd);

        /// <summary>
        /// 保存状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultInfo SaveStatus(UserLogin model);
    }
}
