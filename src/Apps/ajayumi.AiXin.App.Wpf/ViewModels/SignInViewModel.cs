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

using ajayumi.AiXin.App.Wpf.Utils;
using ajayumi.AiXin.CC;
using ajayumi.AiXin.Infrastructure;
using ajayumi.AiXin.Model;
using MahApps.Metro.Controls.Dialogs;
using ReactiveUI;
using Splat;
using System;
using System.Reactive.Linq;
using System.Windows;

namespace ajayumi.AiXin.App.Wpf.ViewModels
{
    public interface ISignInViewModel : IRoutableViewModel
    {
        /// <summary>
        /// 登陆 命令
        /// </summary>
        IReactiveCommand SignInCommand { get; }
        /// <summary>
        /// 切换到注册界面 命令
        /// </summary>
        IReactiveCommand SwitchSignUpCommand { get; }
    }


    /// <summary>
    /// 登录
    /// </summary>
    public class SignInViewModel : ReactiveObject, ISignInViewModel
    {
        private readonly IDialogCoordinator m_DialogCoordinator;

        public IScreen HostScreen { get; protected set; }

        public string UrlPathSegment
        {
            get { return nameof(SignInViewModel); }
        }

        IReactiveCommand ISignInViewModel.SignInCommand
        {
            get
            {
                return SignInCommand;
            }
        }
        public ReactiveCommand<object> SignInCommand { get; set; }

        IReactiveCommand ISignInViewModel.SwitchSignUpCommand
        {
            get
            {
                return SwitchSignUpCommand;
            }
        }
        public ReactiveCommand<object> SwitchSignUpCommand { get; set; }

        private string m_UserName;
        public string UserName
        {
            get { return m_UserName; }
            set { this.RaiseAndSetIfChanged(ref this.m_UserName, value); }
        }

        private string m_Password;
        public string Password
        {
            get { return m_Password; }
            set { this.RaiseAndSetIfChanged(ref this.m_Password, value); }
        }

        public string AppName
        {
            get { return AppConfiger.AppCfg.AppName; }
        }

        public NotifyMsgViewModel NotifyMsgVM { get; set; }

        public WaitingPanelViewModel WaitingPanelVM { get; protected set; }

        public SignInViewModel() { }

        public SignInViewModel(IScreen screen)
        {
            HostScreen = screen;

            this.NotifyMsgVM = new NotifyMsgViewModel();
            this.WaitingPanelVM = Locator.CurrentMutable.GetService<WaitingPanelViewModel>();

            this.SignInCommand = ReactiveCommand.Create(
                this.WhenAny(x => x.UserName, x => x.Password, (x1, x2) => !string.IsNullOrEmpty(x1.Value) && !string.IsNullOrEmpty(x2.Value)));
            this.SwitchSignUpCommand = ReactiveCommand.Create();

            this.SignInCommand.Subscribe(o => this.SignIn(o));
            this.SwitchSignUpCommand.Subscribe(o =>
            {
                this.HostScreen.Router.NavigateCommandFor<SignUpViewModel>().Execute(this.HostScreen);
            });

            if (Loginer.CurrCC == null)
            {
                var cc = Locator.Current.GetService<CC.ICommunication>();
                cc.ConnectedCallbackHandler = this.ConnectedCallback;
                cc.SignInCallbackHandler = this.SignInCallback;
                cc.DownloadAvatarCallbackHandler = this.DownloadAvatarCallback;
                cc.ErrorCallbackHandler = this.ErrorCallback;

                Loginer.CurrCC = cc;

                this.Connect();
            }
        }

        public SignInViewModel(IScreen screen, IDialogCoordinator dialogCoordinator)
            : this(screen)
        {
            m_DialogCoordinator = dialogCoordinator;
        }

        private void Connect()
        {
            this.ToggleWaiting(true);
            Loginer.CurrCC.Connect(new ConnectOptions()
            {
                ServerAddr = AppConfiger.AppCfg.CurrCC_Cfg.ServerAddr,
                ServerPort = AppConfiger.AppCfg.CurrCC_Cfg.ServerPort
            });
        }

        private void SignIn(object o)
        {
            if (!Loginer.CurrCC.IsConnected)
            {
                this.NotifyMsgVM.Msg = "未连接服务器，请检查网络是否畅通。";
                return;
            }
            this.ToggleWaiting(true);
            Loginer.CurrCC.SignIn(new CC.Requests.SignInReq() { Account = this.UserName, Pwd = this.Password });
        }

        private void ConnectedCallback(ResultInfo res)
        {
            this.ToggleWaiting(false);
        }

        private void SignInCallback(ResultInfo<User> res)
        {
            this.ToggleWaiting(false);
            if (!res.Success)
            {
                this.NotifyMsgVM.Msg = res.RMsg;
                return;
            }

            Loginer.CurrUser = res.RData;

            //下载登陆用户头像
            Loginer.CurrCC.DownloadAvatar(Loginer.CurrUser.Id);
        }

        private void DownloadAvatarCallback(ResultInfo<Tuple<long, byte[]>> res)
        {
            if (!res.Success)
            {
                this.NavigateToHome();
                return;
            }

            //保存头像
            try
            {
                FileHelper.SaveAvatar(res.RData.Item2, Loginer.CurrUser?.Avatar);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, AppConfiger.AppCfg.AppName, MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.NavigateToHome();
        }

        private void ErrorCallback(Exception ex)
        {
            Console.WriteLine(ex.Message);
            this.ToggleWaiting(false);
            this.NotifyMsgVM.Msg = ex.Message;
        }

        private void NavigateToHome()
        {
            //跳转到首页
            this.HostScreen.Router.NavigateCommandFor<HomeViewModel>().Execute(this.HostScreen);
        }

        private void ToggleWaiting(bool value)
        {
            this.WaitingPanelVM.WaitingVisibility = value ? Visibility.Visible : Visibility.Hidden;
        }
    }
}
