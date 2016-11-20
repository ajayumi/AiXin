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
using MahApps.Metro.Controls;
using System;
using System.Windows;

namespace ajayumi.AiXin.App.Wpf
{
    public class WindowBase : MetroWindow, IDisposable
    {
        public WindowBase()
            : base()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.FontSize = 14d;
            this.Title = AppConfiger.AppCfg.AppName;

            this.BorderThickness = new Thickness(1);
            this.BorderBrush = null;
            this.SetResourceReference(MetroWindow.GlowBrushProperty, "AccentColorBrush");
        }

        public virtual void Dispose()
        {
            //LoggerManager.Log(ELoggerType.Debug, "{0} 销毁...", this.ToString());
        }
    }
}
