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

using ajayumi.AiXin.App.Wpf.Models;
using ajayumi.AiXin.App.Wpf.Views;
using ajayumi.AiXin.Infrastructure;
using ajayumi.AiXin.Model;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Windows;

namespace ajayumi.AiXin.App.Wpf.ViewModels
{
    public interface IChatViewModel : IRoutableViewModel
    {
        /// <summary>
        /// 删除用户 命令
        /// </summary>
        IReactiveCommand RemoveChatUserCommand { get; }
        /// <summary>
        /// 聊天用户列表选择项改变 命令
        /// </summary>
        IReactiveCommand ChatUserSelectionChangedCommand { get; }

    }

    public class ChatViewModel : ReactiveObject, IChatViewModel
    {
        public IScreen HostScreen { get; protected set; }

        public string UrlPathSegment
        {
            get { return nameof(ChatViewModel); }
        }

        IReactiveCommand IChatViewModel.RemoveChatUserCommand
        {
            get
            {
                return RemoveChatUserCommand;
            }
        }
        public ReactiveCommand<object> RemoveChatUserCommand { get; protected set; }

        IReactiveCommand IChatViewModel.ChatUserSelectionChangedCommand
        {
            get
            {
                return ChatUserSelectionChangedCommand;
            }
        }
        public ReactiveCommand<object> ChatUserSelectionChangedCommand { get; protected set; }

        public ReactiveList<ChatUser> ChatUsers { get; set; }

        public ReactiveList<UcChatDetailView> ChatDetails { get; set; }

        private readonly Dictionary<string, ChatDetailViewModel> m_ChatDetailDict = new Dictionary<string, ChatDetailViewModel>();

        public ChatViewModel() { }

        public ChatViewModel(IScreen screen)
        {
            HostScreen = screen;

            Loginer.CurrCC.GetOnlineListCallbackHandler = this.GetOnlineListCallback;
            Loginer.CurrCC.DownloadAvatarCallbackHandler = this.DownloadAvatarCallback;
            Loginer.CurrCC.TalkCallbackHandler = this.TalkCallback;

            this.RemoveChatUserCommand = ReactiveCommand.Create();
            this.ChatUserSelectionChangedCommand = ReactiveCommand.Create();

            this.RemoveChatUserCommand.Subscribe(o =>
            {
                if (o != null && o is ChatUser)
                {
                    this.RemoveUser((ChatUser)o);
                }
            });

            this.ChatUserSelectionChangedCommand.Subscribe(o => this.ChatUserSelectionChanged(o));

            ChatUsers = new ReactiveList<ChatUser>();
            this.ChatDetails = new ReactiveList<UcChatDetailView>();

            Loginer.CurrCC.GetOnlineList();
        }

        private void ChatUserSelectionChanged(object obj)
        {
            this.m_ChatDetailDict.Values.ToList().ForEach(i => i.ToggleVisibility(false));
            if (obj == null) return;

            var user = (ChatUser)obj;
            string viewName = $"ChatDetail{user.User.Id}";
            if (!this.m_ChatDetailDict.ContainsKey(viewName))
            {
                this.m_ChatDetailDict.Add(viewName, new ChatDetailViewModel(user));

                if (!this.ChatDetails.Any(o => o.Name.Equals(viewName)))
                {
                    this.ChatDetails.Add(new UcChatDetailView() { Name = viewName, DataContext = this.m_ChatDetailDict[viewName] });
                }
            }
            else
            {
                this.m_ChatDetailDict[viewName].ToggleVisibility(true);
            }
            user.UnReadedVisibility = Visibility.Hidden;
        }

        private void TalkCallback(ResultInfo<Message> res)
        {
            if (!res.Success)
            {
                MessageBox.Show(res.RMsg, AppConfiger.AppCfg.AppName, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var ctrl = this.m_ChatDetailDict.Values.FirstOrDefault(o => o.IsActived);
            if (ctrl == null)
            {
                var item = this.ChatUsers.FirstOrDefault(o => o.User.Id == res.RData.FromUser.Id);
                if (item != null)
                {
                    item.UnReadedVisibility = Visibility.Visible;
                }
                return;
            }

            //判断收到的消息来源，是否与当前打开窗口用户一致
            if (res.RData.FromUser.Id != res.RData.ToUser.Id &&
                res.RData.FromUser.Id != ctrl.ChatUser.User.Id)
            {
                var item = this.ChatUsers.FirstOrDefault(o => o.User.Id == res.RData.FromUser.Id);
                if (item != null)
                {
                    item.UnReadedVisibility = Visibility.Visible;
                }

                ctrl = this.m_ChatDetailDict.Values.FirstOrDefault(o => o.ChatUser.User.Id == res.RData.FromUser.Id);
            }

            ctrl?.AddMsg(new ChatMsg()
            {
                Content = res.RData.Content,
                FromUser = new ChatUser() { User = res.RData.FromUser },
                ToUser = new ChatUser() { User = res.RData.ToUser },
                OwnVisibility = Visibility.Hidden,
                ThireVisibility = Visibility.Visible
            });
        }

        private void GetOnlineListCallback(ResultInfo<IEnumerable<User>> res)
        {
            Observable.Start(() =>
            {
                foreach (var item in res.RData)
                {
                    if (!ChatUsers.Any(o => o.User.Name.Equals(item.Name)))
                    {
                        ChatUsers.Add(new ChatUser() { User = item });
                        Loginer.CurrCC.DownloadAvatar(item.Id);
                    }
                }

                var users = this.ChatUsers.Where(o => !res.RData.Any(x => x.Id == o.User.Id)).ToList();
                foreach (var item in users)
                {
                    this.RemoveUser(item);
                }
            }, RxApp.MainThreadScheduler);
        }

        private void DownloadAvatarCallback(ResultInfo<Tuple<long, byte[]>> res)
        {
            if (!res.Success)
            {
                return;
            }

            long userId = res.RData.Item1;
            byte[] data = res.RData.Item2;

            var item = this.ChatUsers.FirstOrDefault(o => o.User.Id == userId);
            if (item == null)
            {
                return;
            }
            var user = item.User;

            string avatarPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, user.Avatar);
            //判断文件是否存在
            if (File.Exists(avatarPath))
            {
                FileInfo file = new FileInfo(avatarPath);
                if (file.Length == data.Length)
                {
                    item.Avatar = avatarPath;
                    return;
                }
            }

            try
            {
                string dir = Path.GetDirectoryName(avatarPath);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                using (FileStream fs = new FileStream(avatarPath, FileMode.OpenOrCreate))
                {
                    fs.Write(data, 0, data.Length);
                }

                item.Avatar = avatarPath;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, AppConfiger.AppCfg.AppName, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="chatUser"></param>
        private void RemoveUser(ChatUser chatUser)
        {
            string viewName = $"ChatDetail{chatUser.User.Id}";
            if (this.m_ChatDetailDict.ContainsKey(viewName))
            {
                var uc = this.ChatDetails.FirstOrDefault(o => o.Name.Equals(viewName));
                this.ChatDetails.Remove(uc);
                this.m_ChatDetailDict.Remove(viewName);
            }
            this.ChatUsers.Remove(chatUser);
        }

        //private void Clear()
        //{
        //    Observable.Start(() => this.ChatUsers.Clear(), RxApp.MainThreadScheduler);
        //}
    }
}
