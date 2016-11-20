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
using System.IO;
using System.Text;

namespace ajayumi.AiXin.Server.Chat.Commands
{
    /// <summary>
    /// 下载头像
    /// </summary>
    [LoggedInValidationFilter()]
    public class DownloadAvatar : CommandBase<ChatSession, BinaryRequestInfo>
    {
        public override string Name
        {
            get
            {
                return Constants.DOWNLOAD_AVATAR_REQUEST_KEY;
            }
        }

        public override void ExecuteCommand(ChatSession session, BinaryRequestInfo requestInfo)
        {
            session.Logger.DebugFormat("Cmd: DownloadAvatar, RemoteEndPoint: {0}", session.RemoteEndPoint);
            string content = Encoding.UTF8.GetString(requestInfo.Body, 0, requestInfo.Body.Length);
            session.Logger.DebugFormat("消息内容：{0}", content);

            ResultInfo<Tuple<long, byte[]>> result = null;
            try
            {
                //解析Json字符串
                var json = JsonDict.Parse(content);
                long userId = json.GetLong("UserId");

                var result2 = BizManager.AutofacBootstrapper.Resolve<IUserBiz>().FindById(userId);
                if (result2.Success)
                {
                    if (!string.IsNullOrEmpty(result2.RData.Avatar))
                    {
                        byte[] data = null;
                        using (FileStream fs = new FileStream(result2.RData.Avatar, FileMode.Open))
                        {
                            data = new byte[fs.Length];
                            fs.Read(data, 0, data.Length);
                        }
                        result = new ResultInfo<Tuple<long, byte[]>>() { RMsg = "下载用户头像成功", RData = new Tuple<long, byte[]>(userId, data) };
                    }
                    else
                    {
                        result = new ResultInfo<Tuple<long, byte[]>>() { RCode = "1001", RMsg = "用户尚未设置头像", Success = false };
                    }
                }
                else
                {
                    result = new ResultInfo<Tuple<long, byte[]>>() { RCode = "1002", RMsg = "用户不存在，下载用户头像失败", Success = false };
                }
            }
            catch (Exception ex)
            {
                result = new ResultInfo<Tuple<long, byte[]>>(ex);
            }

            ArraySegmentWrapper segmentWrapper = new ArraySegmentWrapper(Constants.DOWNLOAD_AVATAR_RESPONSE_KEY, JConverter.SerializeToBytes(result));
            session.SendData(session, segmentWrapper.Wrapper());
        }
    }
}
