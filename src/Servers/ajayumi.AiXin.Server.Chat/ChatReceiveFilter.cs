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

using System;
using System.Text;
using SuperSocket.Common;
using SuperSocket.Facility.Protocol;
using SuperSocket.SocketBase.Protocol;

namespace ajayumi.AiXin.Server.Chat
{
    public class ChatReceiveFilter : FixedHeaderReceiveFilter<BinaryRequestInfo>
    {
        public ChatReceiveFilter()
            : base(4 + 8)
        {

        }

        protected override int GetBodyLengthFromHeader(byte[] header, int offset, int length)
        {
            //var len = (int)header[offset + 4] * 1024 + (int)header[offset + 5];
            var lenStr = Encoding.ASCII.GetString(header, offset + 4, 8);
            var len = Int32.Parse(lenStr, System.Globalization.NumberStyles.HexNumber);

            return len;
        }

        protected override BinaryRequestInfo ResolveRequestInfo(ArraySegment<byte> header, byte[] bodyBuffer, int offset, int length)
        {
            var request = new BinaryRequestInfo(Encoding.ASCII.GetString(header.Array, header.Offset, 4), bodyBuffer.CloneRange(offset, length));
            return request;
        }
    }
}