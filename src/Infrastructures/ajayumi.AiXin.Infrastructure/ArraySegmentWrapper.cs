using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ajayumi.AiXin.Infrastructure
{
    /// <summary>
    /// ArraySegment 包装器
    /// </summary>
    public sealed class ArraySegmentWrapper
    {
        public string Cmd { get; set; }

        //public string Message { get; set; }

        public long TotalLength { get; set; }

        public byte[] Data { get; set; }

        public int DataOffset
        {
            get { return 4 + 8; }
        }


        public ArraySegment<byte> Segment { get; private set; }

        //public ArraySegmentWrapper(string cmd, string message)
        //{
        //    this.Cmd = cmd;
        //    this.Message = message;
        //}

        public ArraySegmentWrapper(string cmd, byte[] data)
        {
            this.Cmd = cmd;
            this.Data = data;
        }

        public ArraySegmentWrapper(byte[] data)
        {
            this.Cmd = Encoding.ASCII.GetString(data, 0, 4);
            var lenStr = Encoding.ASCII.GetString(data, 4, 8);
            this.TotalLength = Int64.Parse(lenStr, System.Globalization.NumberStyles.HexNumber);
        }


        public ArraySegment<byte> Wrapper()
        {
            byte[] cmd = Encoding.ASCII.GetBytes(this.Cmd);
            byte[] data;
            if (this.Data != null && this.Data.Length > 0)
            {
                data = this.Data;
            }
            //else if (!string.IsNullOrEmpty(this.Message))
            //{
            //    data = Encoding.UTF8.GetBytes(this.Message);
            //}
            else
            {
                data = new byte[0];
            }
            byte[] dataLen = Encoding.ASCII.GetBytes(data.Length.ToString("X8"));

            byte[] result = new byte[cmd.Length + dataLen.Length + data.Length];
            Buffer.BlockCopy(cmd, 0, result, 0, cmd.Length);
            Buffer.BlockCopy(dataLen, 0, result, cmd.Length, dataLen.Length);
            Buffer.BlockCopy(data, 0, result, cmd.Length + dataLen.Length, data.Length);

            ArraySegment<byte> segment = new ArraySegment<byte>(result);
            this.Segment = segment;
            return segment;
        }
    }
}
