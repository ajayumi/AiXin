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
    /// <summary>
    /// 注册
    /// </summary>
    public class SignUp : CommandBase<ChatSession, BinaryRequestInfo>
    {
        public override string Name
        {
            get
            {
                return Constants.SIGNUP_REQUEST_KEY;
            }
        }

        public override void ExecuteCommand(ChatSession session, BinaryRequestInfo requestInfo)
        {
            session.Logger.DebugFormat("Cmd: SignUp, RemoteEndPoint: {0}", session.RemoteEndPoint);
            string content = Encoding.UTF8.GetString(requestInfo.Body, 0, requestInfo.Body.Length);
            session.Logger.DebugFormat("消息内容：{0}", content);

            ArraySegmentWrapper segmentWrapper = null;
            ResultInfo<User> result = null;
            try
            {
                //解析Json字符串
                var json = JsonDict.Parse(content);
                string name = json["Name"];
                string email = json["Email"];
                string pwd = json["Pwd"];
                //数据库验证
                result = BizManager.AutofacBootstrapper.Resolve<IAuthBiz>().SignUp(name, email, pwd);
            }
            catch (Exception ex)
            {
                result = new ResultInfo<User>(ex);
            }

            //发送注册响应消息
            segmentWrapper = new ArraySegmentWrapper(Constants.SIGNUP_RESPONSE_KEY, JConverter.SerializeToBytes(result));
            session.SendData(session, segmentWrapper.Wrapper());
        }
    }
}
