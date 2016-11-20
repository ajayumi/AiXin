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
using ReactiveUI;
using System;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ajayumi.AiXin.App.Wpf.ViewModels
{
    public class ChatDetailViewModel : ReactiveObject
    {
        private ChatUser m_ChatUser = null;
        public ChatUser ChatUser { get { return this.m_ChatUser; } }

        private Visibility m_ViewVisibility = Visibility.Hidden;
        public Visibility ViewVisibility
        {
            get { return m_ViewVisibility; }
            set { this.RaiseAndSetIfChanged(ref this.m_ViewVisibility, value); }
        }

        private string m_Caption = string.Empty;
        public string Caption
        {
            get { return m_Caption; }
            set { this.RaiseAndSetIfChanged(ref this.m_Caption, value); }
        }

        private string m_Content = string.Empty;
        public string Content
        {
            get { return m_Content; }
            set { this.RaiseAndSetIfChanged(ref this.m_Content, value); }
        }

        public ReactiveList<ChatMsg> m_ReceivedMsgs;
        public IReadOnlyReactiveList<ChatMsg> ReceivedMsgs => m_ReceivedMsgs;

        public bool IsActived { get; private set; }

        /// <summary>
        /// 发送消息 命令
        /// </summary>
        public ReactiveCommand<object> SendCommand { get; set; }


        public ChatDetailViewModel()
        {
            m_ReceivedMsgs = new ReactiveList<ChatMsg>();

            this.SendCommand = ReactiveCommand.Create();

            this.SendCommand.Subscribe(o => this.Send(o));

        }

        public ChatDetailViewModel(ChatUser user)
            : this()
        {
            m_ChatUser = user;
            this.Caption = $"与 {user.CustomName} 对话";
            this.ToggleVisibility(true);
        }

        internal void Close()
        {
            m_ChatUser = null;

            this.Caption = string.Empty;
            this.ToggleVisibility(false);
        }

        internal void ToggleVisibility(bool value)
        {
            this.ViewVisibility = value ? Visibility.Visible : Visibility.Hidden;
            this.IsActived = value;
        }

        internal void AddMsg(ChatMsg msg)
        {
            Observable.Start(() =>
            {
                m_ReceivedMsgs.Add(msg);
            }, RxApp.MainThreadScheduler);
        }

        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="o"></param>
        private void Send(object o)
        {
            string content = this.Content;
            if (string.IsNullOrEmpty(content) && o != null)
            {
                string text = string.Empty;
                if (o is TextBox)
                {
                    var ctrl = (TextBox)o;
                    text = ctrl.Text.Trim();
                    ctrl.Text = string.Empty;
                }
                else if (o is Xceed.Wpf.Toolkit.RichTextBox)
                {
                    var ctrl = (Xceed.Wpf.Toolkit.RichTextBox)o;
                    text = ctrl.Text.Trim();
                    ctrl.Text = string.Empty;
                }
                content = text;
            }
            if (string.IsNullOrEmpty(content))
            {
                return;
            }

            this.AddMsg(new ChatMsg()
            {
                Content = content,
                FromUser = new ChatUser() { User = Loginer.CurrUser },
                OwnVisibility = Visibility.Visible,
                ThireVisibility = Visibility.Hidden
            });

            Loginer.CurrCC.Talk(new CC.Requests.TalkReq()
            {
                Content = content,
                FromUId = Loginer.CurrUser.Id.ToString(),
                ToUId = this.m_ChatUser.User.Id.ToString()
            });

            this.Content = string.Empty;
        }

    }
}
