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

using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;

namespace ajayumi.AiXin.Server.Chat
{
    public class ChatServer : AppServer<ChatSession, BinaryRequestInfo>
    {
        public ChatServer()
            : base(new DefaultReceiveFilterFactory<ChatReceiveFilter, BinaryRequestInfo>())
        {

        }
    }
}
