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
using System.Collections.Generic;
using System.Text;

namespace ajayumi.AiXin.Server.Chat.Commands
{
    /// <summary>
    /// 获取通讯录
    /// </summary>
    [LoggedInValidationFilter()]
    public class GetContacts : CommandBase<ChatSession, BinaryRequestInfo>
    {
        public override string Name
        {
            get
            {
                return Constants.GET_CONTACTS_REQUEST_KEY;
            }
        }

        public override void ExecuteCommand(ChatSession session, BinaryRequestInfo requestInfo)
        {
            session.Logger.DebugFormat("Cmd: GetContacts, RemoteEndPoint: {0}", session.RemoteEndPoint);
            string content = Encoding.UTF8.GetString(requestInfo.Body, 0, requestInfo.Body.Length);
            session.Logger.DebugFormat("消息内容：{0}", content);

            ResultInfo<IEnumerable<Contacts>> result = null;
            try
            {
                //解析Json字符串
                var json = JsonDict.Parse(content);
                long oUserId = json.GetLong("OUserId");
                string keyword = json["Keyword"];
                int pageNum = json.GetInt("PageNum");
                int pageSize = json.GetInt("PageSize");

                result = BizManager.AutofacBootstrapper.Resolve<IContactsBiz>().GetDatas(oUserId, keyword, keyword, keyword, keyword, keyword, pageNum, pageSize);
            }
            catch (Exception ex)
            {
                result = new ResultInfo<IEnumerable<Contacts>>(ex);
            }
            ArraySegmentWrapper segmentWrapper = new ArraySegmentWrapper(Constants.GET_CONTACTS_RESPONSE_KEY, JConverter.SerializeToBytes(result));
            session.SendData(session, segmentWrapper.Wrapper());
        }
    }
}
