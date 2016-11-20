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

using System.Net;

namespace ajayumi.AiXin.CC
{
    public class ConnectOptions
    {
        /// <summary>
        /// 服务地址
        /// </summary>
        public string ServerAddr { get; set; }

        /// <summary>
        /// 服务端口
        /// </summary>
        public int ServerPort { get; set; }

        /// <summary>
        /// 凭证
        /// </summary>
        public NetworkCredential Credential { get; set; }


        /// <summary>
        /// 通信服务器终结点
        /// </summary>
        public EndPoint ServerEP
        {
            get
            {
                EndPoint ep = null;
                IPAddress ipAddr = IPAddress.Any;
                if (IPAddress.TryParse(this.ServerAddr, out ipAddr))
                {
                    ep = new IPEndPoint(ipAddr, this.ServerPort);
                }
                else
                {
                    ep = new DnsEndPoint(this.ServerAddr, this.ServerPort);
                }
                return ep;
            }
        }

    }
}
