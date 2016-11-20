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
using System.Windows;
using System.Windows.Media;

namespace ajayumi.AiXin.App.Wpf.ViewModels
{
    public class NotifyMsgViewModel : ReactiveObject
    {
        private Brush m_Background = new SolidColorBrush(Colors.Red);
        public Brush Background
        {
            get { return m_Background; }
            set { this.RaiseAndSetIfChanged(ref m_Background, value); }
        }

        private Brush m_Foreground = new SolidColorBrush(Colors.White);
        public Brush Foreground
        {
            get { return m_Foreground; }
            set { this.RaiseAndSetIfChanged(ref m_Foreground, value); }
        }

        private Visibility m_Visibility = Visibility.Hidden;
        public Visibility Visibility
        {
            get { return m_Visibility; }
            set { this.RaiseAndSetIfChanged(ref m_Visibility, value); }
        }

        private string m_Msg;
        public string Msg
        {
            get { return m_Msg; }
            set
            {
                this.RaiseAndSetIfChanged(ref m_Msg, value);
                this.Visibility = Visibility.Visible;
            }
        }

        public ReactiveCommand<object> NotifyMsgClickCommand { get; set; }

        public NotifyMsgViewModel()
        {
            this.NotifyMsgClickCommand = ReactiveCommand.Create();
            this.NotifyMsgClickCommand.Subscribe(_ =>
            {
                this.Visibility = Visibility.Hidden;
            });
        }
    }
}
