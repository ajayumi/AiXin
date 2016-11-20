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
using ajayumi.AiXin.Server.Chat.Filters;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Text;

namespace ajayumi.AiXin.Server.Chat.Commands
{
    /// <summary>
    /// 保存通讯录
    /// </summary>
    [LoggedInValidationFilter()]
    public class SaveContacts : CommandBase<ChatSession, BinaryRequestInfo>
    {
        public override string Name
        {
            get
            {
                return Constants.SAVE_CONTACTS_REQUEST_KEY;
            }
        }

        public override void ExecuteCommand(ChatSession session, BinaryRequestInfo requestInfo)
        {
            session.Logger.DebugFormat("Cmd: SaveContacts, RemoteEndPoint: {0}", session.RemoteEndPoint);
            string content = Encoding.UTF8.GetString(requestInfo.Body, 0, requestInfo.Body.Length);
            session.Logger.DebugFormat("消息内容：{0}", content);

            ResultInfo result = null;
            try
            {
                //解析Json字符串
                var json = JsonDict.Parse(content);
                long oUserId = json.GetLong("OUserId");
                long cUserId = json.GetLong("CUserId");
                string customName = json["CustomName"];

                result = BizManager.AutofacBootstrapper.Resolve<IContactsBiz>().Save(oUserId, cUserId, customName);
            }
            catch (Exception ex)
            {
                result = new ResultInfo(ex);
            }
            ArraySegmentWrapper segmentWrapper = new ArraySegmentWrapper(Constants.SAVE_CONTACTS_RESPONSE_KEY, JConverter.SerializeToBytes(result));
            session.SendData(session, segmentWrapper.Wrapper());
        }
    }
}
