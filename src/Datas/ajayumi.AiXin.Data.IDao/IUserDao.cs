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

using ajayumi.AiXin.Model;

namespace ajayumi.AiXin.Data.IDao
{
    public interface IUserDao : IDaoBase<User>
    {
        /// <summary>
        /// 根据用户名查找
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        User FindByName(string name);
        /// <summary>
        /// 根据电子邮箱查找
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        User FindByEmail(string email);
        /// <summary>
        /// 根据手机号查找
        /// </summary>
        /// <param name="telphone"></param>
        /// <returns></returns>
        User FindByTelphone(string telphone);

        /// <summary>
        /// 更新用户状态
        /// </summary>
        /// <param name="user"></param>
        void UpdateStatus(User user);

        /// <summary>
        /// 更新用户头像
        /// </summary>
        /// <param name="id"></param>
        /// <param name="avatar"></param>
        void UpdateAvatar(long id, string avatar);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pwd"></param>
        void ChangePwd(long id, string pwd);

        /// <summary>
        /// 添加用户登录信息
        /// </summary>
        /// <param name="model"></param>
        void AddUserLogin(UserLogin model);
    }
}
