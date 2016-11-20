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
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Linq;

namespace ajayumi.AiXin.Server.Chat
{
    public class ChatSession : AppSession<ChatSession, BinaryRequestInfo>
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public User User { get; set; }

        protected override void OnSessionStarted()
        {
            this.Logger.InfoFormat("客户端 {0}/{1} 连入", this.SessionID, this.RemoteEndPoint);

            ResultInfo result = new ResultInfo() { RMsg = string.Format("{0} 您已进入联网监控区域", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")) };
            ArraySegmentWrapper segmentWrapper = new ArraySegmentWrapper(Constants.CONNECTED_RESPONSE_KEY, JConverter.SerializeToBytes(result));

            this.SendData(this, segmentWrapper.Wrapper());
        }

        protected override void HandleException(Exception ex)
        {
            this.Logger.ErrorFormat("应用程序异常: {0}", ex.Message);

            var json = new JsonDict();
            json.Add("SessionID", this.SessionID);
            json.Add("Msg", string.Format("{0} 应用程序异常，异常信息：{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ex.Message));
            ArraySegmentWrapper segmentWrapper = new ArraySegmentWrapper(Constants.ERROR_RESPONSE_KEY, json.ToBytes());

            this.SendData(this, segmentWrapper.Wrapper());
        }

        protected override void OnSessionClosed(CloseReason reason)
        {
            this.Logger.InfoFormat("客户端 {0}/{1} 断开连接，原因：{2}", this.SessionID, this.RemoteEndPoint, reason);

            if (this.User != null)
            {
                this.User.ConnectState = EConnectState.Offline;
                UserLogin model = new UserLogin() { UserId = this.User.Id, IP = this.RemoteEndPoint.ToString(), CustomId = this.SessionID, ConnectState = EConnectState.Offline };
                BizManager.AutofacBootstrapper.Resolve<IAuthBiz>().SaveStatus(model);
                this.NotifyOffline(this.SessionID, this.User.Name);
            }
            base.OnSessionClosed(reason);
        }


        /// <summary>
        /// 提醒上线
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="userName"></param>
        internal void NotifyOnline(string sessionId, string userName)
        {
            var ss = AppServer.GetSessions(o => !o.SessionID.Equals(sessionId));
            ss.AsParallel().ForAll(s =>
            {
                var json = new JsonDict();
                json.Add("SessionID", sessionId);
                json.Add("UserName", userName);
                json.Add("Msg", string.Format("{0} 客户端上线", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                ArraySegmentWrapper segmentWrapper = new ArraySegmentWrapper(Constants.NOTIFY_ONLINE_RESPONSE_KEY, json.ToBytes());

                this.SendData(s, segmentWrapper.Wrapper());
            });
        }

        /// <summary>
        /// 提醒下线
        /// </summary>
        /// <param name="sessionId"></param>
        internal void NotifyOffline(string sessionId, string userName)
        {
            var ss = AppServer.GetSessions(o => !o.SessionID.Equals(sessionId));
            ss?.AsParallel().ForAll(s =>
            {
                var json = new JsonDict();
                json.Add("SessionID", sessionId);
                json.Add("UserName", userName);
                json.Add("Msg", string.Format("{0} 客户端下线", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                ArraySegmentWrapper segmentWrapper = new ArraySegmentWrapper(Constants.NOTIFY_OFFLINE_RESPONSE_KEY, json.ToBytes());

                this.SendData(s, segmentWrapper.Wrapper());
            });
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="session"></param>
        /// <param name="segment"></param>
        internal void SendData(ChatSession session, ArraySegment<byte> segment)
        {
            try
            {
                int count = segment.Count;
                int maxRequestLength = AppConfiger.AppCfg.CurrCC_Cfg.MaxRequestLength;
                if (count > maxRequestLength)
                {
                    int offset = 0;
                    while (offset < count)
                    {
                        session.Send(segment.Array, offset, Math.Min(maxRequestLength, count - offset));

                        offset += maxRequestLength;
                    }
                }
                else
                {
                    session.Send(segment);
                }
            }
            catch (Exception ex)
            {
                var json = new JsonDict();
                json.Add("SessionID", session.SessionID);
                json.Add("Msg", string.Format("{0} 发送数据异常，异常信息：{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ex.Message));
                ArraySegmentWrapper segmentWrapper = new ArraySegmentWrapper(Constants.ERROR_RESPONSE_KEY, json.ToBytes());
                session.Send(segmentWrapper.Wrapper());
            }
        }

    }
}
