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
using ajayumi.AiXin.Model;
using System;
using System.Collections.Generic;

namespace ajayumi.AiXin.CC
{
    /// <summary>
    /// 通信接口
    /// </summary>
    public interface ICommunication
    {
        bool IsConnected { get; }

        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <param name="options"></param>
        void Connect(ConnectOptions options);

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="req"></param>
        void SignIn(SignInReq req);

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="req"></param>
        void SignUp(SignUpReq req);

        /// <summary>
        /// 获取在线用户列表
        /// </summary>
        void GetOnlineList();

        /// <summary>
        /// 对话
        /// </summary>
        /// <param name="req"></param>
        void Talk(TalkReq req);

        /// <summary>
        /// 搜索用户
        /// </summary>
        /// <param name="account">用户名、手机号、电子邮箱</param>
        void SearchUser(string account);

        /// <summary>
        /// 保存通讯录（若没有则新增，若存在则更新）
        /// </summary>
        /// <param name="oUserId"></param>
        /// <param name="cUserId"></param>
        /// <param name="customName"></param>
        void SaveContacts(long oUserId, long cUserId, string customName);

        /// <summary>
        /// 获取通讯录集合
        /// </summary>
        /// <param name="oUserId">所属人</param>
        /// <param name="keyword">用户名、昵称、备注</param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        void GetContacts(long oUserId, string keyword, int pageNum, int pageSize);

        /// <summary>
        /// 修改个人资料
        /// </summary>
        /// <param name="user"></param>
        void EditProfile(User user);

        /// <summary>
        /// 上传用户头像
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="extName"></param>
        /// <param name="data"></param>
        void UploadAvatar(long userId, string extName, byte[] data);

        /// <summary>
        /// 下载用户头像
        /// </summary>
        /// <param name="userId"></param>
        void DownloadAvatar(long userId);

        /// <summary>
        /// 断开连接
        /// </summary>
        void Disconnect();

        /// <summary>
        /// 连接服务器成功
        /// </summary>
        Action<ResultInfo> ConnectedCallbackHandler { get; set; }

        /// <summary>
        /// 登录成功委托
        /// </summary>
        Action<ResultInfo<User>> SignInCallbackHandler { get; set; }

        /// <summary>
        /// 注册成功委托
        /// </summary>
        Action<ResultInfo> SignUpCallbackHandler { get; set; }

        /// <summary>
        /// 获取在线用户委托
        /// </summary>
        Action<ResultInfo<IEnumerable<User>>> GetOnlineListCallbackHandler { get; set; }

        /// <summary>
        /// 对话消息委托
        /// </summary>
        Action<ResultInfo<Message>> TalkCallbackHandler { get; set; }

        /// <summary>
        /// 搜索用户委托
        /// </summary>
        Action<ResultInfo<IEnumerable<User>>> SearchUserCallbackHandler { get; set; }

        /// <summary>
        /// 保存通讯录委托
        /// </summary>
        Action<ResultInfo> SaveContactsCallbackHandler { get; set; }

        /// <summary>
        /// 获取通讯录列表委托
        /// </summary>
        Action<ResultInfo<IEnumerable<Contacts>>> GetContactsCallbackHandler { get; set; }

        /// <summary>
        /// 修改个人资料委托
        /// </summary>
        Action<ResultInfo> EditProfileCallbackHandler { get; set; }

        /// <summary>
        /// 上传头像委托
        /// </summary>
        Action<ResultInfo> UploadAvatarCallbackHandler { get; set; }

        /// <summary>
        /// 下载头像委托
        /// </summary>
        Action<ResultInfo<Tuple<long, byte[]>>> DownloadAvatarCallbackHandler { get; set; }

        /// <summary>
        /// 收到消息委托
        /// </summary>
        Action<ReceivedRes> ReceivedCallbackHandler { get; set; }

        /// <summary>
        /// 异常委托
        /// </summary>
        Action<Exception> ErrorCallbackHandler { get; set; }
    }
}
