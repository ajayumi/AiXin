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

using ajayumi.AiXin.Infrastructure;
using ajayumi.AiXin.Infrastructure.Json;
using ajayumi.AiXin.Infrastructure.SuperSocket;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Metadata;

namespace ajayumi.AiXin.Server.Chat.Filters
{
    /// <summary>
    /// 登陆验证过滤器
    /// </summary>
    class LoggedInValidationFilter : CommandFilterAttribute
    {
        public override void OnCommandExecuting(CommandExecutingContext commandContext)
        {
            var session = commandContext.Session as ChatSession;

            //If the session is not logged in, cancel the executing of the command
            if (session.User == null)
            {
                ResultInfo result = new ResultInfo() { RMsg = $"未登陆，命令执行失败。" };
                ArraySegmentWrapper segmentWrapper = new ArraySegmentWrapper(Constants.ERROR_PERMIT_RESPONSE_KEY, JConverter.SerializeToBytes(result));
                session.Send(segmentWrapper.Wrapper());

                session.Close();
                commandContext.Cancel = true;
            }
        }

        public override void OnCommandExecuted(CommandExecutingContext commandContext)
        {

        }
    }
}
