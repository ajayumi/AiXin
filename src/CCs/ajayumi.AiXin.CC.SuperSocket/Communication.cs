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

using ajayumi.AiXin.CC.Requests;
using ajayumi.AiXin.CC.Responses;
using ajayumi.AiXin.Infrastructure;
using ajayumi.AiXin.Infrastructure.Json;
using ajayumi.AiXin.Infrastructure.SuperSocket;
using ajayumi.AiXin.Model;
using SuperSocket.ClientEngine;
using SuperSocket.ProtoBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ajayumi.AiXin.CC.SuperSocket
{
    public class Communication : ICommunication
    {
        private const int MAX_RECONNECT_COUNT = 3;    //最大重连次数

        private int m_ReconnectCount = 0;   //重连次数

        private readonly EasyClient<BufferedPackageInfo> m_Client = null;

        /// <summary>
        /// 是否连接服务器
        /// </summary>
        public bool IsConnected { get; private set; }

        public ConnectOptions ConnectOptions { get; protected set; }

        public Action<ResultInfo> ConnectedCallbackHandler { get; set; }
        public Action<ResultInfo<User>> SignInCallbackHandler { get; set; }
        public Action<ResultInfo> SignUpCallbackHandler { get; set; }
        public Action<ResultInfo<IEnumerable<User>>> GetOnlineListCallbackHandler { get; set; }
        public Action<ResultInfo<Message>> TalkCallbackHandler { get; set; }
        public Action<ResultInfo<IEnumerable<User>>> SearchUserCallbackHandler { get; set; }
        public Action<ResultInfo> SaveContactsCallbackHandler { get; set; }
        public Action<ResultInfo<IEnumerable<Contacts>>> GetContactsCallbackHandler { get; set; }
        public Action<ResultInfo> EditProfileCallbackHandler { get; set; }
        public Action<ResultInfo> UploadAvatarCallbackHandler { get; set; }
        public Action<ResultInfo<Tuple<long, byte[]>>> DownloadAvatarCallbackHandler { get; set; }
        public Action<ReceivedRes> ReceivedCallbackHandler { get; set; }
        public Action<Exception> ErrorCallbackHandler { get; set; }

        public Communication()
        {
            m_Client = new EasyClient<BufferedPackageInfo>();
            m_Client.ReceiveBufferSize = AppConfiger.AppCfg.CurrCC_Cfg.MaxRequestLength;
            //m_Client.ReceiveBufferSize = 1024 * 1024 * 10;
            m_Client.Connected += TcpSession_Connected;
            m_Client.NewPackageReceived += TcpSession_PackageReceived; ;
            m_Client.Error += (sender, e) =>
            {
                //重连
                if (!this.IsConnected)
                {
                    if (this.m_ReconnectCount < MAX_RECONNECT_COUNT)
                    {
                        this.m_ReconnectCount++;
                        Console.WriteLine("{0} 尝试重连，5秒后重试……", this.m_ReconnectCount);
                        Thread.Sleep(1000 * 5);
                        this.Connect(this.ConnectOptions);
                    }
                    else
                    {
                        this.ErrorCallbackHandler?.Invoke(e.Exception);
                    }
                }
            };
            m_Client.Initialize(new ReceiveFilter());
        }

        private void TcpSession_PackageReceived(object sender, PackageEventArgs<BufferedPackageInfo> e)
        {
            string cmd = e.Package.Key;
            //ArraySegment<byte> data = e.Package.Data.LastOrDefault();
            //string msg = Encoding.UTF8.GetString(data.Array, data.Offset, data.Count);

            var lst = (BufferList)e.Package.Data;
            byte[] data = lst.Skip(1).SelectMany((o, i) => (i == 0) ? o.Array.Skip(o.Offset) : o.Array).ToArray();
            int total = lst.Skip(1).Sum(o => o.Count);
            string msg = Encoding.UTF8.GetString(data, 0, total);
            this.DataAnalyze(cmd, msg);
        }

        private void DataAnalyze(string cmd, string msg)
        {
            JsonDict json = null;
            JsonDictCollection jsons = null;
            switch (cmd)
            {
                case Constants.CONNECTED_RESPONSE_KEY:
                    //建立连接响应报文
                    var res1 = JConverter.Deserialize<ResultInfo>(msg);
                    this.ConnectedCallbackHandler?.Invoke(res1);
                    break;
                case Constants.SIGNIN_RESPONSE_KEY:
                    //用户登录响应报文
                    var res2 = JConverter.Deserialize<ResultInfo<User>>(msg);
                    this.SignInCallbackHandler?.Invoke(res2);
                    break;
                case Constants.SIGNUP_RESPONSE_KEY:
                    //用户注册
                    var res3 = JConverter.Deserialize<ResultInfo>(msg);
                    this.SignUpCallbackHandler?.Invoke(res3);
                    break;
                case Constants.ONLINE_LIST_RESPONSE_KEY:
                    //获取在线用户列表
                    var res4 = JConverter.Deserialize<ResultInfo<IEnumerable<User>>>(msg);
                    this.GetOnlineListCallbackHandler?.Invoke(res4);
                    break;
                case Constants.TALK_RESPONSE_KEY:
                    //对话
                    var res5 = JConverter.Deserialize<ResultInfo<Message>>(msg);
                    this.TalkCallbackHandler?.Invoke(res5);
                    break;
                case Constants.SEARCH_USER_RESPONSE_KEY:
                    //搜索用户
                    var res6 = JConverter.Deserialize<ResultInfo<IEnumerable<User>>>(msg);
                    this.SearchUserCallbackHandler?.Invoke(res6);
                    break;
                case Constants.SAVE_CONTACTS_RESPONSE_KEY:
                    //保存通讯录
                    var res7 = JConverter.Deserialize<ResultInfo>(msg);
                    this.SaveContactsCallbackHandler?.Invoke(res7);
                    break;
                case Constants.GET_CONTACTS_RESPONSE_KEY:
                    //获取通讯录
                    var res8 = JConverter.Deserialize<ResultInfo<IEnumerable<Contacts>>>(msg);
                    this.GetContactsCallbackHandler?.Invoke(res8);
                    break;
                case Constants.EDIT_PROFILE_RESPONSE_KEY:
                    //修改个人资料
                    var res9 = JConverter.Deserialize<ResultInfo>(msg);
                    this.EditProfileCallbackHandler?.Invoke(res9);
                    break;
                case Constants.UPLOAD_AVATAR_RESPONSE_KEY:
                    //上传头像
                    var res10 = JConverter.Deserialize<ResultInfo>(msg);
                    this.UploadAvatarCallbackHandler?.Invoke(res10);
                    break;
                case Constants.DOWNLOAD_AVATAR_RESPONSE_KEY:
                    //下载头像
                    var res11 = JConverter.Deserialize<ResultInfo<Tuple<long, byte[]>>>(msg);
                    this.DownloadAvatarCallbackHandler?.Invoke(res11);
                    break;
                case Constants.NOTIFY_ONLINE_RESPONSE_KEY:
                case Constants.NOTIFY_OFFLINE_RESPONSE_KEY:
                    this.GetOnlineList();
                    break;
                default:
                    var res99 = new ReceivedRes() { FromUser = "Your", ToUser = "Me", Content = msg };
                    this.ReceivedCallbackHandler?.Invoke(res99);
                    break;
            }
        }

        private void TcpSession_Connected(object sender, EventArgs e)
        {
            Console.WriteLine("连接服务器成功");
            this.IsConnected = true;
        }

        public void Connect(ConnectOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException("服务器连接选项不能为空");
            }

            ConnectOptions = options;
            if (!this.m_Client.IsConnected)
            {
                try
                {
                    m_Client.ConnectAsync(this.ConnectOptions.ServerEP);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private void Send(ArraySegmentWrapper segmentWrapper)
        {
            if (this.IsConnected && !this.m_Client.IsConnected)
            {
                //断线重连
                this.Connect(this.ConnectOptions);
                return;
            }

            try
            {
                ArraySegment<byte> segment = segmentWrapper.Wrapper();
                int count = segment.Count;
                int maxRequestLength = AppConfiger.AppCfg.CurrCC_Cfg.MaxRequestLength;
                if (count > maxRequestLength)
                {
                    int offset = 0;
                    while (offset < count)
                    {
                        ArraySegment<byte> tmp = new ArraySegment<byte>(segment.Skip(offset).Take(maxRequestLength).ToArray());
                        m_Client.Send(tmp);
                        Thread.Sleep(50);

                        offset += maxRequestLength;
                    }
                }
                else
                {
                    m_Client.Send(segment);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SignIn(SignInReq req)
        {
            ArraySegmentWrapper segmentWrapper = new ArraySegmentWrapper(Constants.SIGNIN_REQUEST_KEY, JConverter.SerializeToBytes(req));
            this.Send(segmentWrapper);
        }

        public void SignUp(SignUpReq req)
        {
            ArraySegmentWrapper segmentWrapper = new ArraySegmentWrapper(Constants.SIGNUP_REQUEST_KEY, JConverter.SerializeToBytes(req));
            this.Send(segmentWrapper);
        }

        public void GetOnlineList()
        {
            ArraySegmentWrapper segmentWrapper = new ArraySegmentWrapper(Constants.ONLINE_LIST_REQUEST_KEY, null);
            this.Send(segmentWrapper);
        }

        public void Talk(TalkReq req)
        {
            var json = new JsonDict();
            json.Add("FromUId", req.FromUId);
            json.Add("ToUId", req.ToUId);
            json.Add("Content", req.Content);

            ArraySegmentWrapper segmentWrapper = new ArraySegmentWrapper(Constants.TALK_REQUEST_KEY, json.ToBytes());
            this.Send(segmentWrapper);
        }

        public void SearchUser(string account)
        {
            var json = new JsonDict();
            json.Add("Account", account);

            ArraySegmentWrapper segmentWrapper = new ArraySegmentWrapper(Constants.SEARCH_USER_REQUEST_KEY, json.ToBytes());
            this.Send(segmentWrapper);
        }

        public void SaveContacts(long oUserId, long cUserId, string customName)
        {
            var json = new JsonDict();
            json.AddLong("OUserId", oUserId);
            json.AddLong("CUserId", cUserId);
            json.Add("CustomName", customName);

            ArraySegmentWrapper segmentWrapper = new ArraySegmentWrapper(Constants.SAVE_CONTACTS_REQUEST_KEY, json.ToBytes());
            this.Send(segmentWrapper);
        }

        public void GetContacts(long oUserId, string keyword, int pageNum, int pageSize)
        {
            var json = new JsonDict();
            json.AddLong("OUserId", oUserId);
            json.Add("Keyword", keyword);
            json.AddInt("PageNum", pageNum);
            json.AddInt("PageSize", pageSize);

            ArraySegmentWrapper segmentWrapper = new ArraySegmentWrapper(Constants.GET_CONTACTS_REQUEST_KEY, json.ToBytes());
            this.Send(segmentWrapper);
        }

        public void EditProfile(User user)
        {
            ArraySegmentWrapper segmentWrapper = new ArraySegmentWrapper(Constants.EDIT_PROFILE_REQUEST_KEY, JConverter.SerializeToBytes(user));
            this.Send(segmentWrapper);
        }

        public void UploadAvatar(long userId, string extName, byte[] data)
        {
            var json = new JsonDict();
            json.AddLong("UserId", userId);
            json.Add("ExtName", extName);
            json.Add("Data", Convert.ToBase64String(data));

            ArraySegmentWrapper segmentWrapper = new ArraySegmentWrapper(Constants.UPLOAD_AVATAR_REQUEST_KEY, JConverter.SerializeToBytes(json));
            this.Send(segmentWrapper);
        }

        public void DownloadAvatar(long userId)
        {
            var json = new JsonDict();
            json.AddLong("UserId", userId);

            ArraySegmentWrapper segmentWrapper = new ArraySegmentWrapper(Constants.DOWNLOAD_AVATAR_REQUEST_KEY, JConverter.SerializeToBytes(json));
            this.Send(segmentWrapper);
        }

        public void Disconnect()
        {
            if (this.IsConnected)
            {
                m_Client.Close();
            }
        }
    }
}
