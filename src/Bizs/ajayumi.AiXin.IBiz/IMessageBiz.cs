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
    public interface IMessageBiz
    {
        /// <summary>
        /// 保存消息
        /// </summary>
        /// <param name="fromUserId"></param>
        /// <param name="toUserId"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        ResultInfo<Message> Save(long fromUserId, long toUserId, string content);
    }
}
