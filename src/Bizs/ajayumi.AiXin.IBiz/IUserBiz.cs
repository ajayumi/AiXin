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
using System.Collections.Generic;

namespace ajayumi.AiXin.IBiz
{
    public interface IUserBiz
    {
        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        ResultInfo<IEnumerable<User>> SearchUser(string account);

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        ResultInfo UpdateUser(User user);

        /// <summary>
        /// 查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ResultInfo<User> FindById(long id);

        /// <summary>
        /// 更新用户头像
        /// </summary>
        /// <param name="id"></param>
        /// <param name="avatar"></param>
        /// <returns></returns>
        ResultInfo UpdateUserAvatar(long id, string avatar);
    }
}
