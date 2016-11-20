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

using ajayumi.AiXin.IBiz;
using ajayumi.AiXin.Infrastructure;
using ajayumi.AiXin.Infrastructure.Json;
using ajayumi.AiXin.Infrastructure.SuperSocket;
using ajayumi.AiXin.Model;
using ajayumi.AiXin.Server.Chat.Filters;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Linq;
using System.Text;

namespace ajayumi.AiXin.Server.Chat.Commands
{
    /// <summary>
    /// 对话
    /// </summary>
    [LoggedInValidationFilter()]
    public class Talk : CommandBase<ChatSession, BinaryRequestInfo>
    {
        public override string Name
        {
            get
            {
                return Constants.TALK_REQUEST_KEY;
            }
        }

        public override void ExecuteCommand(ChatSession session, BinaryRequestInfo requestInfo)
        {
            session.Logger.DebugFormat("Cmd: Talk, RemoteEndPoint: {0}", session.RemoteEndPoint);
            string content = Encoding.UTF8.GetString(requestInfo.Body, 0, requestInfo.Body.Length);
            session.Logger.DebugFormat("消息内容：{0}", content);

            ResultInfo<Message> result = null;
            long toUId = 0L;
            try
            {
                //解析Json字符串
                var json = JsonDict.Parse(content);
                long fromUId = json.GetLong("FromUId");
                toUId = json.GetLong("ToUId");
                string msg = json["Content"];

                result = BizManager.AutofacBootstrapper.Resolve<IMessageBiz>().Save(fromUId, toUId, msg);
            }
            catch (Exception ex)
            {
                result = new ResultInfo<Message>(ex);
            }

            ArraySegmentWrapper segmentWrapper = new ArraySegmentWrapper(Constants.TALK_RESPONSE_KEY, JConverter.SerializeToBytes(result));
            var s = session.AppServer.GetSessions(o => o.User?.Id == toUId).FirstOrDefault();
            session.SendData(s, segmentWrapper.Wrapper());
        }
    }
}
