using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace ajayumi.AiXin.App.Wpf
{
    public class DialogHelper
    {
        //从Handle中获取Window对象
        static Window GetWindowFromHwnd(IntPtr hwnd)
        {
            return (Window)HwndSource.FromHwnd(hwnd).RootVisual;
        }

        //GetForegroundWindow API
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        //调用GetForegroundWindow然后调用GetWindowFromHwnd
        static Window GetTopWindow()
        {
            var hwnd = GetForegroundWindow();
            if (hwnd == null)
                return null;

            return GetWindowFromHwnd(hwnd);
        }

        //显示对话框并自动设置Owner
        public static bool? ShowDialog(Window win)
        {
            win.BorderThickness = new Thickness(1);
            win.BorderBrush = null;
            win.SetResourceReference(MetroWindow.GlowBrushProperty, "AccentColorBrush");

            win.Owner = GetTopWindow();
            win.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            win.ShowInTaskbar = false;
            return win.ShowDialog();
        }
    }
}
