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

using ajayumi.AiXin.Infrastructure;
using ReactiveUI;

namespace ajayumi.AiXin.App.Wpf.ViewModels
{
    public interface IWelcomeViewModel : IRoutableViewModel
    {

    }

    public class WelcomeViewModel : ReactiveObject, IWelcomeViewModel
    {
        public IScreen HostScreen { get; protected set; }

        public string UrlPathSegment
        {
            get { return nameof(WelcomeViewModel); }
        }

        public string AppName
        {
            get { return AppConfiger.AppCfg.AppName; }
        }

        public WelcomeViewModel() { }

        public WelcomeViewModel(IScreen screen)
        {
            HostScreen = screen;
        }
    }
}
