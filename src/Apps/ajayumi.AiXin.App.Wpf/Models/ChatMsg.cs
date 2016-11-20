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

using ReactiveUI;
using System;
using System.IO;
using System.Windows;

namespace ajayumi.AiXin.App.Wpf.Models
{
    public class ChatMsg : ReactiveObject
    {
        public ChatUser FromUser { get; set; }

        private string m_FromUserAvatar = string.Empty;
        public string FromUserAvatar
        {
            get
            {
                m_FromUserAvatar = "pack://application:,,,/ajayumi.AiXin.App.Wpf;component/Images/user_head.jpg";
                if (!string.IsNullOrEmpty(FromUser.Avatar))
                {
                    string tmpPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FromUser.Avatar);
                    if (File.Exists(tmpPath))
                    {
                        m_FromUserAvatar = tmpPath;
                    }
                }
                return m_FromUserAvatar;
            }
            set { this.RaiseAndSetIfChanged(ref this.m_FromUserAvatar, value); }
        }

        public ChatUser ToUser { get; set; }

        public Visibility ThireVisibility { get; set; }

        public Visibility OwnVisibility { get; set; }

        public string Content { get; set; }

        public string CreationDTime { get { return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); } }

    }
}
