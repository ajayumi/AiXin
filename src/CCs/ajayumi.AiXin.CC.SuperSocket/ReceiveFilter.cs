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

using SuperSocket.ProtoBase;
using System;
using System.Linq;
using System.Text;

namespace ajayumi.AiXin.CC.SuperSocket
{
    public class ReceiveFilter : FixedHeaderReceiveFilter<BufferedPackageInfo>
    {
        public ReceiveFilter()
            : base(4 + 8)
        {
        }

        public override BufferedPackageInfo ResolvePackage(IBufferStream bufferStream)
        {
            var header = bufferStream.Buffers.FirstOrDefault();
            string key = Encoding.ASCII.GetString(header.Array, header.Offset, 4);
            return new BufferedPackageInfo(key, bufferStream.Buffers);
        }

        protected override int GetBodyLengthFromHeader(IBufferStream bufferStream, int length)
        {
            var header = bufferStream.Buffers.FirstOrDefault();
            var lenStr = Encoding.ASCII.GetString(header.Array, header.Offset + 4, 8);
            var len = Int32.Parse(lenStr, System.Globalization.NumberStyles.HexNumber);

            return len;
        }
    }
}