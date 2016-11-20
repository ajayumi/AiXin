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
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Text;

namespace ajayumi.AiXin.Server.Chat.Commands
{
    public class SignIn : CommandBase<ChatSession, BinaryRequestInfo>
    {
        public override string Name
        {
            get
            {
                return Constants.SIGNIN_REQUEST_KEY;
            }
        }

        public override void ExecuteCommand(ChatSession session, BinaryRequestInfo requestInfo)
        {
            session.Logger.DebugFormat("Cmd: SignIn, RemoteEndPoint: {0}", session.RemoteEndPoint);
            string content = Encoding.UTF8.GetString(requestInfo.Body, 0, requestInfo.Body.Length);
            session.Logger.DebugFormat("消息内容：{0}", content);

            ArraySegmentWrapper segmentWrapper = null;
            ResultInfo<User> result = null;
            try
            {
                //解析Json字符串
                var json = JsonDict.Parse(content);
                string account = json["Account"];
                string pwd = json["Pwd"];
                //数据库验证
                result = BizManager.AutofacBootstrapper.Resolve<IAuthBiz>().SignIn(account, pwd);

                if (result.Success)
                {
                    result.RData.Password = string.Empty;
                    session.User = result.RData;
                    session.User.ConnectState = EConnectState.Online;
                    UserLogin model = new UserLogin() { UserId = result.RData.Id, IP = session.RemoteEndPoint.ToString(), CustomId = session.SessionID, ConnectState = EConnectState.Online };
                    BizManager.AutofacBootstrapper.Resolve<IAuthBiz>().SaveStatus(model);

                    //通知有客户端连入
                    session.NotifyOnline(session.SessionID, account);
                }
            }
            catch (Exception ex)
            {
                result = new ResultInfo<User>(ex);
            }

            //发送登录响应消息
            segmentWrapper = new ArraySegmentWrapper(Constants.SIGNIN_RESPONSE_KEY, JConverter.SerializeToBytes(result));
            session.SendData(session, segmentWrapper.Wrapper());
        }
    }
}
