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
using Splat;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Windows;

namespace ajayumi.AiXin.App.Wpf.ViewModels
{
    public interface ISignUpViewModel : IRoutableViewModel
    {
        /// <summary>
        /// 注册 命令
        /// </summary>
        IReactiveCommand SignUpCommand { get; }
        /// <summary>
        /// 切换到登录界面 命令
        /// </summary>
        IReactiveCommand SwitchSignInCommand { get; }

    }

    /// <summary>
    /// 注册
    /// </summary>
    public class SignUpViewModel : ReactiveValidatedObject, ISignUpViewModel
    {
        public IScreen HostScreen { get; protected set; }

        public string UrlPathSegment
        {
            get { return nameof(SignUpViewModel); }
        }

        IReactiveCommand ISignUpViewModel.SignUpCommand
        {
            get
            {
                return SignUpCommand;
            }
        }

        public ReactiveCommand<object> SignUpCommand { get; set; }

        IReactiveCommand ISignUpViewModel.SwitchSignInCommand
        {
            get
            {
                return SwitchSignInCommand;
            }
        }
        public ReactiveCommand<object> SwitchSignInCommand { get; set; }

        private string m_UserName;
        [Display(Name = "用户名")]
        [ValidatesViaMethod(AllowBlanks = false, AllowNull = false, Name = "IsNameValid")]
        public string UserName
        {
            get { return m_UserName; }
            set { this.RaiseAndSetIfChanged(ref this.m_UserName, value); }
        }

        private string m_Email;
        [Display(Name = "电子邮箱")]
        [ValidatesViaMethod(AllowBlanks = false, AllowNull = false, Name = "IsEmailValid", ErrorMessage = "电子邮箱 格式有误")]
        public string Email
        {
            get { return m_Email; }
            set { this.RaiseAndSetIfChanged(ref this.m_Email, value); }
        }

        private string m_Password;
        [Display(Name = "密码")]
        [ValidatesViaMethod(AllowBlanks = false, AllowNull = false, Name = "IsPasswordValid")]
        public string Password
        {
            get { return m_Password; }
            set { this.RaiseAndSetIfChanged(ref this.m_Password, value); }
        }

        private string m_Password2;
        [Display(Name = "确认密码")]
        [ValidatesViaMethod(AllowBlanks = false, AllowNull = false, Name = "IsPassword2Valid")]
        public string Password2
        {
            get { return m_Password2; }
            set { this.RaiseAndSetIfChanged(ref this.m_Password2, value); }
        }

        public string AppName
        {
            get { return AppConfiger.AppCfg.AppName; }
        }

        public NotifyMsgViewModel NotifyMsgVM { get; set; }

        public WaitingPanelViewModel WaitingPanelVM { get; protected set; }

        public SignUpViewModel() { }

        public SignUpViewModel(IScreen screen)
        {
            HostScreen = screen;

            this.NotifyMsgVM = new NotifyMsgViewModel();
            this.WaitingPanelVM = Locator.CurrentMutable.GetService<WaitingPanelViewModel>();

            this.SignUpCommand = ReactiveCommand.Create(this.WhenAny(
                x => x.UserName, x => x.Email, x => x.Password, x => x.Password2,
                (x1, x2, x3, x4) => !string.IsNullOrEmpty(x1.Value) && !string.IsNullOrEmpty(x2.Value) && !string.IsNullOrEmpty(x3.Value) && !string.IsNullOrEmpty(x4.Value)));
            this.SwitchSignInCommand = ReactiveCommand.Create();

            this.SignUpCommand.Subscribe(o => this.SignUp(o));
            this.SwitchSignInCommand.Subscribe(o =>
            {
                this.HostScreen.Router.NavigateCommandFor<SignInViewModel>().Execute(this.HostScreen);
            });

            Loginer.CurrCC.SignUpCallbackHandler += (res) =>
            {
                this.ToggleWaiting(false);
                if (res.Success)
                {
                    this.HostScreen.Router.NavigateCommandFor<SignInViewModel>().Execute(this.HostScreen);
                }
                else
                {
                    this.NotifyMsgVM.Msg = res.RMsg;
                }
            };
        }

        public bool IsNameValid(string name)
        {
            return !string.IsNullOrEmpty(name);
        }

        public bool IsEmailValid(string email)
        {
            return Regex.IsMatch(email, @"^\w+@\w+\.\w+$");
        }

        public bool IsPasswordValid(string pwd)
        {
            return !string.IsNullOrEmpty(pwd);
        }

        public bool IsPassword2Valid(string pwd)
        {
            return !string.IsNullOrEmpty(pwd);
        }


        private void SignUp(object obj)
        {
            if (string.IsNullOrEmpty(this.UserName))
            {
                this.NotifyMsgVM.Msg = "用户名不能为空";
                return;
            }
            if (string.IsNullOrEmpty(this.Email))
            {
                this.NotifyMsgVM.Msg = "电子邮箱不能为空";
                return;
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                this.NotifyMsgVM.Msg = "密码不能为空";
                return;
            }
            if (string.IsNullOrEmpty(this.Password2))
            {
                this.NotifyMsgVM.Msg = "确认密码不能为空";
                return;
            }
            if (!this.Password.Equals(this.Password2))
            {
                this.NotifyMsgVM.Msg = "两次输入的密码不一致";
                return;

            }

            this.ToggleWaiting(true);
            Loginer.CurrCC.SignUp(new CC.Requests.SignUpReq() { Name = this.UserName, Email = this.Email, Pwd = this.Password });
        }

        private void ToggleWaiting(bool value)
        {
            this.WaitingPanelVM.WaitingVisibility = value ? Visibility.Visible : Visibility.Hidden;
        }

    }
}
