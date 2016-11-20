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
    public interface IContactsBiz
    {
        /// <summary>
        /// 添加用户到通讯录
        /// </summary>
        /// <param name="oUserId"></param>
        /// <param name="cUserId"></param>
        /// <param name="customName"></param>
        /// <returns></returns>
        ResultInfo Save(long oUserId, long cUserId, string customName);

        /// <summary>
        /// 获取通讯录列表
        /// </summary>
        /// <param name="oUserId">所属人</param>
        /// <param name="cUserName">用户名</param>
        /// <param name="cNickName">昵称</param>
        /// <param name="cTelphone">手机号</param>
        /// <param name="cEmail">电子邮箱</param>
        /// <param name="customName">备注名称</param>
        /// <param name="pageNum">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns></returns>
        ResultInfo<IEnumerable<Contacts>> GetDatas(long oUserId, string cUserName, string cNickName, string cTelphone, string cEmail, string customName, int pageNum, int pageSize);
    }
}
