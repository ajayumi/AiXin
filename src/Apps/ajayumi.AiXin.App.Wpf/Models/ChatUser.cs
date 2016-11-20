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

using ajayumi.AiXin.Model;
using ReactiveUI;
using System;
using System.IO;
using System.Windows;

namespace ajayumi.AiXin.App.Wpf.Models
{
    public class ChatUser : ReactiveObject
    {
        public User User { get; set; }

        private string m_Avatar = string.Empty;
        public string Avatar
        {
            get
            {
                m_Avatar = "pack://application:,,,/ajayumi.AiXin.App.Wpf;component/Images/user_head.jpg";
                if (!string.IsNullOrEmpty(User.Avatar))
                {
                    string tmpPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, User.Avatar);
                    if (File.Exists(tmpPath))
                    {
                        m_Avatar = tmpPath;
                    }
                }
                return m_Avatar;
            }
            set { this.RaiseAndSetIfChanged(ref this.m_Avatar, value); }
        }

        public string CustomName
        {
            get
            {
                string name = this.User.NickName;
                if (string.IsNullOrEmpty(name))
                {
                    name = this.User.Name;
                }

                return name;
            }
        }

        private Visibility m_UnReadedVisibility = Visibility.Hidden;

        public Visibility UnReadedVisibility
        {
            get { return m_UnReadedVisibility; }
            set { this.RaiseAndSetIfChanged(ref this.m_UnReadedVisibility, value); }
        }


        public string UpdatedDate { get { return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); } }
    }
}
