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
using MahApps.Metro;
using System.Globalization;
using System.Threading;
using System.Windows;

namespace ajayumi.AiXin.App.Wpf
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("zh-CN");
            Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var appAccent = ThemeManager.GetAccent(AppConfiger.AppCfg.AppAccent);
            var appTheme = ThemeManager.GetAppTheme(AppConfiger.AppCfg.AppTheme);
            ThemeManager.ChangeAppStyle(Application.Current, appAccent, appTheme);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            if (Loginer.CurrCC.IsConnected)
            {
                Loginer.SignOut();
            }
            base.OnExit(e);
        }
    }
}
