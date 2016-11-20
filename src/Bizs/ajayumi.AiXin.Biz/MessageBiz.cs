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
using System;

namespace ajayumi.AiXin.Biz
{
    public class MessageBiz : IMessageBiz
    {
        private readonly IMessageDao m_Dao = null;

        public MessageBiz(IMessageDao dao)
        {
            this.m_Dao = dao;
        }

        public ResultInfo<Message> Save(long fromUserId, long toUserId, string content)
        {
            ResultInfo<Message> result = null;
            try
            {
                var msg = this.m_Dao.Add(new Message() { FromUserId = fromUserId, ToUserId = toUserId, Content = content });
                msg = this.m_Dao.FindById(msg.Id);
                result = new ResultInfo<Message>() {  RMsg = "保存消息成功", RData = msg };
            }
            catch (Exception ex)
            {
                result = new ResultInfo<Message>(ex);
            }
            return result;
        }
    }
}
