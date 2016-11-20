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

namespace ajayumi.AiXin.App.Wpf.ViewModels
{
    public class MainViewModel : ReactiveObject
    {
        public IScreen HostScreen { get; protected set; }

        public ReactiveCommand<object> SettingCommand { get; set; }

        public MainViewModel()
        {
        }

        public MainViewModel(IScreen screen)
            : this()
        {
            HostScreen = screen;

            this.SettingCommand = ReactiveCommand.Create();

            this.SettingCommand.Subscribe(o =>
            {
                //DialogHelper.ShowDialog(new SettingWindow() { Content = new SettingView() { DataContext = Locator.CurrentMutable.GetService<SettingViewModel>() } });
            });

        }

       
    }
}
