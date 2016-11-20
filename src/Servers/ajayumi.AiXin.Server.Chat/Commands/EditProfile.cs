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
using System.Text;

namespace ajayumi.AiXin.Server.Chat.Commands
{
    /// <summary>
    /// 修改个人资料
    /// </summary>
    [LoggedInValidationFilter()]
    public class EditProfile : CommandBase<ChatSession, BinaryRequestInfo>
    {
        public override string Name
        {
            get
            {
                return Constants.EDIT_PROFILE_REQUEST_KEY;
            }
        }

        public override void ExecuteCommand(ChatSession session, BinaryRequestInfo requestInfo)
        {
            session.Logger.DebugFormat("Cmd: EditProfile, RemoteEndPoint: {0}", session.RemoteEndPoint);
            string content = Encoding.UTF8.GetString(requestInfo.Body, 0, requestInfo.Body.Length);
            session.Logger.DebugFormat("消息内容：{0}", content);

            ResultInfo result = null;
            try
            {
                //解析Json字符串
                var user = JConverter.Deserialize<User>(content);

                result = BizManager.AutofacBootstrapper.Resolve<IUserBiz>().UpdateUser(user);
            }
            catch (Exception ex)
            {
                result = new ResultInfo(ex);
            }

            ArraySegmentWrapper segmentWrapper = new ArraySegmentWrapper(Constants.EDIT_PROFILE_RESPONSE_KEY, JConverter.SerializeToBytes(result));
            session.SendData(session, segmentWrapper.Wrapper());
        }
    }
}
