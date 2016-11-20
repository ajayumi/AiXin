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
    /// 上传头像
    /// </summary>
    [LoggedInValidationFilter()]
    public class UploadAvatar : CommandBase<ChatSession, BinaryRequestInfo>
    {
        public override string Name
        {
            get
            {
                return Constants.UPLOAD_AVATAR_REQUEST_KEY;
            }
        }

        public override void ExecuteCommand(ChatSession session, BinaryRequestInfo requestInfo)
        {
            session.Logger.DebugFormat("Cmd: UploadAvatar, RemoteEndPoint: {0}", session.RemoteEndPoint);
            string content = Encoding.UTF8.GetString(requestInfo.Body, 0, requestInfo.Body.Length);
            //session.Logger.DebugFormat("消息内容：{0}", content);

            ResultInfo result = null;
            try
            {
                //解析Json字符串
                var json = JsonDict.Parse(content);
                long userId = json.GetLong("UserId");
                string extName = json["ExtName"];
                string dataStr = json["Data"];
                byte[] data = Convert.FromBase64String(dataStr);

                var result2 = BizManager.AutofacBootstrapper.Resolve<IUserBiz>().FindById(userId);
                if (result2.Success)
                {
                    string dir = Path.Combine("Users", "Avatar", result2.RData.Name);
                    string path = Path.Combine(dir, $"{DateTime.Now.ToString("yyyyMMdd")}_{Guid.NewGuid()}{extName}");
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                    {
                        fs.Write(data, 0, data.Length);
                    }

                    var result3 = BizManager.AutofacBootstrapper.Resolve<IUserBiz>().UpdateUserAvatar(userId, path);
                    if (result3.Success)
                    {
                        result = new ResultInfo() {  RMsg = "用户头像上传成功" };
                    }
                    else
                    {
                        result = new ResultInfo() { RCode = "1002", RMsg = "用户头像上传失败", Success = false };
                    }
                }
                else
                {
                    result = new ResultInfo() { RCode = "1001", RMsg = "用户不存在，上传用户头像失败", Success = false };
                }
            }
            catch (Exception ex)
            {
                result = new ResultInfo(ex);
            }

            ArraySegmentWrapper segmentWrapper = new ArraySegmentWrapper(Constants.UPLOAD_AVATAR_RESPONSE_KEY, JConverter.SerializeToBytes(result));
            session.SendData(session, segmentWrapper.Wrapper());
        }
    }
}
