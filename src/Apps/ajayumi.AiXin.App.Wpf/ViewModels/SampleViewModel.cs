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

using MahApps.Metro;
using ReactiveUI;
using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows;

namespace ajayumi.AiXin.App.Wpf.ViewModels
{
    public class SampleViewModel : ReactiveObject
    {
        public ReactiveCommand<Unit> ChangeStyleCommand { get; set; }

        public SampleViewModel()
        {
            this.ChangeStyleCommand = ReactiveCommand.CreateAsyncObservable(x => ChangeStyle());
            this.ChangeStyleCommand.ThrownExceptions.Subscribe(ex => Console.WriteLine(ex));
        }

        private IObservable<Unit> ChangeStyle()
        {
            return Observable.Start(() =>
            {
                var theme = ThemeManager.DetectAppStyle(Application.Current);
                ThemeManager.ChangeAppStyle(Application.Current, ThemeManager.GetAccent("Red"), theme.Item1);
            },RxApp.MainThreadScheduler); 
        }
    }
}
