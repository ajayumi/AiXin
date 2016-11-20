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

using ajayumi.AiXin.App.Wpf.Models;
using ajayumi.AiXin.Infrastructure;
using MahApps.Metro;
using MahApps.Metro.Controls.Dialogs;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace ajayumi.AiXin.App.Wpf.ViewModels
{
    public interface ISettingViewModel : IRoutableViewModel
    {
        IReactiveCommand SaveCommand { get; }
        IReactiveCommand CancelCommand { get; }
        IReactiveCommand AccentColorSelectionChangedCommand { get; }
        IReactiveCommand AppThemeSelectionChangedCommand { get; }
    }

    public class SettingViewModel : ReactiveObject, ISettingViewModel
    {
        private readonly IDialogCoordinator m_DialogCoordinator;

        public IScreen HostScreen { get; protected set; }

        public string UrlPathSegment
        {
            get { return nameof(SettingViewModel); }
        }

        IReactiveCommand ISettingViewModel.SaveCommand
        {
            get { return SaveCommand; }
        }

        IReactiveCommand ISettingViewModel.CancelCommand
        {
            get { return CancelCommand; }
        }

        IReactiveCommand ISettingViewModel.AccentColorSelectionChangedCommand
        {
            get { return AccentColorSelectionChangedCommand; }
        }

        IReactiveCommand ISettingViewModel.AppThemeSelectionChangedCommand
        {
            get { return AppThemeSelectionChangedCommand; }
        }

        public ReactiveCommand<object> SaveCommand { get; protected set; }

        public ReactiveCommand<object> CancelCommand { get; protected set; }

        public ReactiveCommand<object> AccentColorSelectionChangedCommand { get; protected set; }

        public ReactiveCommand<object> AppThemeSelectionChangedCommand { get; protected set; }

        public List<AccentColorData> AccentColors { get; set; }

        public List<AppThemeData> AppThemes { get; set; }

        private AppThemeData m_CurrAppTheme = null;
        private AccentColorData m_CurrAppAccent = null;

        public SettingViewModel() { }

        public SettingViewModel(IScreen screen)
        {
            HostScreen = screen;

            this.SaveCommand = ReactiveCommand.Create();
            this.SaveCommand.Subscribe(p =>
            {
                if (this.m_CurrAppTheme != null)
                { AppConfiger.AppCfg.AppTheme = this.m_CurrAppTheme.Name; }
                if (this.m_CurrAppAccent != null)
                { AppConfiger.AppCfg.AppAccent = this.m_CurrAppAccent.Name; }

                AppConfiger.Save();

                m_DialogCoordinator.ShowMessageAsync(this, AppConfiger.AppCfg.AppName, "设置保存成功", MessageDialogStyle.Affirmative, new MetroDialogSettings() { AffirmativeButtonText = "确定" });
            });

            this.CancelCommand = ReactiveCommand.Create();
            this.CancelCommand.Subscribe(p =>
            {
                HostScreen.Router.NavigateCommandFor<HomeViewModel>().Execute(this.HostScreen);
                //HostScreen.Router.NavigateBack.Execute(null);
            });

            this.AccentColorSelectionChangedCommand = ReactiveCommand.Create();
            this.AccentColorSelectionChangedCommand.Subscribe(p => ChangeAccentColor(p));

            this.AppThemeSelectionChangedCommand = ReactiveCommand.Create();
            this.AppThemeSelectionChangedCommand.Subscribe(p => ChangeAppTheme(p));

            this.AccentColors = ThemeManager.Accents.Select(a => new AccentColorData() { Name = a.Name, ColorBrush = a.Resources["AccentColorBrush"] as Brush }).ToList();
            this.AppThemes = ThemeManager.AppThemes.Select(a => new AppThemeData() { Name = a.Name, BorderColorBrush = a.Resources["BlackColorBrush"] as Brush, ColorBrush = a.Resources["WhiteColorBrush"] as Brush }).ToList();
        }

        public SettingViewModel(IScreen screen, IDialogCoordinator dialogCoordinator)
            : this(screen)
        {
            m_DialogCoordinator = dialogCoordinator;
        }

        /// <summary>
        /// 修改样式
        /// </summary>
        /// <param name="obj"></param>
        private void ChangeAccentColor(object obj)
        {
            if (obj != null && obj is AccentColorData)
            {
                m_CurrAppAccent = (obj as AccentColorData);
                var theme = ThemeManager.DetectAppStyle(Application.Current);
                var accent = ThemeManager.GetAccent(m_CurrAppAccent.Name);
                ThemeManager.ChangeAppStyle(Application.Current, accent, theme.Item1);
            }
        }

        /// <summary>
        /// 修改风格
        /// </summary>
        /// <param name="obj"></param>
        private void ChangeAppTheme(object obj)
        {
            if (obj != null && obj is AppThemeData)
            {
                m_CurrAppTheme = (obj as AppThemeData);
                var theme = ThemeManager.DetectAppStyle(Application.Current);
                var appTheme = ThemeManager.GetAppTheme(m_CurrAppTheme.Name);
                ThemeManager.ChangeAppStyle(Application.Current, theme.Item2, appTheme);
            }
        }
    }


}
