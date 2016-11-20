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
using System.Windows;

namespace ajayumi.AiXin.App.Wpf.ViewModels
{
    public class WaitingPanelViewModel : ReactiveObject
    {

        private Visibility m_WaitingVisibility = Visibility.Hidden;
        public Visibility WaitingVisibility
        {
            get { return m_WaitingVisibility; }
            set { this.RaiseAndSetIfChanged(ref this.m_WaitingVisibility, value); }
        }

        public WaitingPanelViewModel()
        {

        }

    }
}
