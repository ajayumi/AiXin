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
using System.Collections.Generic;
using System.Windows;

namespace ajayumi.AiXin.App.Wpf.ViewModels
{
    public interface IHomeViewModel : IRoutableViewModel
    {
        /// <summary>
        /// 消息 点击命令
        /// </summary>
        IReactiveCommand ChatCommand { get; }
        /// <summary>
        /// 群组 点击命令
        /// </summary>
        IReactiveCommand GroupCommand { get; }
        /// <summary>
        /// 通讯录 点击命令
        /// </summary>
        IReactiveCommand ContactsCommand { get; }
        /// <summary>
        /// 搜索 点击命令
        /// </summary>
        IReactiveCommand SearchCommand { get; }
        /// <summary>
        /// 设置 点击命令
        /// </summary>
        IReactiveCommand SettingCommand { get; }
        /// <summary>
        /// 注销 点击命令
        /// </summary>
        IReactiveCommand SignOutCommand { get; }
        /// <summary>
        /// 个人资料 点击命令
        /// </summary>
        IReactiveCommand ProfileCommand { get; }

    }

    public class HomeViewModel : ReactiveObject, IHomeViewModel
    {
        public RoutingState SubRouter { get; private set; }

        public IScreen HostScreen { get; protected set; }

        public string UrlPathSegment
        {
            get { return nameof(HomeViewModel); }
        }

        IReactiveCommand IHomeViewModel.ChatCommand
        {
            get { return ChatCommand; }
        }
        public ReactiveCommand<object> ChatCommand { get; set; }

        IReactiveCommand IHomeViewModel.GroupCommand
        {
            get { return GroupCommand; }
        }
        public ReactiveCommand<object> GroupCommand { get; set; }

        IReactiveCommand IHomeViewModel.ContactsCommand
        {
            get { return ContactsCommand; }
        }
        public ReactiveCommand<object> ContactsCommand { get; set; }

        IReactiveCommand IHomeViewModel.SearchCommand
        {
            get { return SearchCommand; }
        }
        public ReactiveCommand<object> SearchCommand { get; set; }

        IReactiveCommand IHomeViewModel.SettingCommand
        {
            get { return SettingCommand; }
        }
        public ReactiveCommand<object> SettingCommand { get; set; }

        IReactiveCommand IHomeViewModel.SignOutCommand
        {
            get { return SignOutCommand; }
        }
        public ReactiveCommand<object> SignOutCommand { get; set; }

        IReactiveCommand IHomeViewModel.ProfileCommand
        {
            get { return ProfileCommand; }
        }
        public ReactiveCommand<object> ProfileCommand { get; set; }


        private bool m_ChatChecked = false;
        public bool ChatChecked
        {
            get { return this.m_ChatChecked; }
            set { this.RaiseAndSetIfChanged(ref this.m_ChatChecked, value); }
        }

        private bool m_GroupChecked = false;
        public bool GroupChecked
        {
            get { return this.m_GroupChecked; }
            set { this.RaiseAndSetIfChanged(ref this.m_GroupChecked, value); }
        }

        private bool m_ContactsChecked = false;
        public bool ContactsChecked
        {
            get { return this.m_ContactsChecked; }
            set { this.RaiseAndSetIfChanged(ref this.m_ContactsChecked, value); }
        }

        private bool m_SearchChecked = false;
        public bool SearchChecked
        {
            get { return this.m_SearchChecked; }
            set { this.RaiseAndSetIfChanged(ref this.m_SearchChecked, value); }
        }

        private bool m_SettingChecked = false;
        public bool SettingChecked
        {
            get { return this.m_SettingChecked; }
            set { this.RaiseAndSetIfChanged(ref this.m_SettingChecked, value); }
        }

        /// <summary>
        /// 登陆用户名
        /// </summary>
        public string LoginerName
        {
            get
            {
                string name = Loginer.CurrUser.NickName;
                if (string.IsNullOrEmpty(name))
                {
                    name = Loginer.CurrUser.Name;
                }
                return name;
            }
        }

        /// <summary>
        /// 登陆用户头像
        /// </summary>
        public string LoginerAvatar
        {
            get { return Loginer.CurrUserAvatar; }
        }

        public string AppName
        {
            get { return AppConfiger.AppCfg.AppName; }
        }

        private readonly Dictionary<string, IRoutableViewModel> m_VModelDict = null;

        public HomeViewModel() { }

        public HomeViewModel(IScreen screen)
        {
            HostScreen = screen;

            SubRouter = new RoutingState();

            this.m_VModelDict = new Dictionary<string, IRoutableViewModel>();

            this.ChatCommand = ReactiveCommand.Create();
            this.GroupCommand = ReactiveCommand.Create();
            this.ContactsCommand = ReactiveCommand.Create();
            this.SearchCommand = ReactiveCommand.Create();
            this.SettingCommand = ReactiveCommand.Create();
            this.SignOutCommand = ReactiveCommand.Create();
            this.ProfileCommand = ReactiveCommand.Create();

            this.ChatCommand.Subscribe(x =>
            {
                this.ChatChecked = true;
                this.GroupChecked = false;
                this.ContactsChecked = false;
                this.SearchChecked = false;
                this.SettingChecked = false;

                this.NavigateTo(nameof(ChatViewModel));
            });

            this.GroupCommand.Subscribe(x =>
            {
                this.ChatChecked = false;
                this.GroupChecked = true;
                this.ContactsChecked = false;
                this.SearchChecked = false;
                this.SettingChecked = false;

                this.NavigateTo(nameof(GroupViewModel));
            });

            this.ContactsCommand.Subscribe(x =>
            {
                this.ChatChecked = false;
                this.GroupChecked = false;
                this.ContactsChecked = true;
                this.SearchChecked = false;
                this.SettingChecked = false;

                this.NavigateTo(nameof(ContactsViewModel));
            });

            this.SearchCommand.Subscribe(x =>
            {
                this.ChatChecked = false;
                this.GroupChecked = false;
                this.ContactsChecked = false;
                this.SearchChecked = true;
                this.SettingChecked = false;

                this.NavigateTo(nameof(SearchViewModel));
            });

            this.SettingCommand.Subscribe(x =>
            {
                this.ChatChecked = false;
                this.GroupChecked = false;
                this.ContactsChecked = false;
                this.SearchChecked = false;
                this.SettingChecked = true;

                this.NavigateTo(nameof(SettingViewModel));
            });

            this.SignOutCommand.Subscribe(x =>
            {
                if (MessageBox.Show("确定要注销吗？", AppConfiger.AppCfg.AppName, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes)
                {
                    this.m_VModelDict.Clear();
                    HostScreen.Router.NavigateCommandFor<SignInViewModel>().Execute(null);
                    Loginer.SignOut();
                }
            });

            this.ProfileCommand.Subscribe(x =>
            {
                this.ChatChecked = false;
                this.GroupChecked = false;
                this.ContactsChecked = false;
                this.SearchChecked = false;
                this.SettingChecked = false;

                this.NavigateTo(nameof(ProfileViewModel));
            });

            this.NavigateTo(nameof(WelcomeViewModel));
        }


        /// <summary>
        /// 导航至页面
        /// </summary>
        /// <param name="vModel"></param>
        private void NavigateTo(string urlPathSegment)
        {
            IRoutableViewModel vm = null;
            if (this.m_VModelDict.ContainsKey(urlPathSegment))
            {
                vm = this.m_VModelDict[urlPathSegment];
            }
            else
            {
                if (urlPathSegment.Equals(nameof(ChatViewModel)))
                {
                    vm = Locator.CurrentMutable.GetService<ChatViewModel>();
                }
                else if (urlPathSegment.Equals(nameof(GroupViewModel)))
                {
                    vm = Locator.CurrentMutable.GetService<GroupViewModel>();
                }
                else if (urlPathSegment.Equals(nameof(ContactsViewModel)))
                {
                    vm = Locator.CurrentMutable.GetService<ContactsViewModel>();
                }
                else if (urlPathSegment.Equals(nameof(SearchViewModel)))
                {
                    vm = Locator.CurrentMutable.GetService<SearchViewModel>();
                }
                else if (urlPathSegment.Equals(nameof(SettingViewModel)))
                {
                    vm = Locator.CurrentMutable.GetService<SettingViewModel>();
                }
                else if (urlPathSegment.Equals(nameof(ProfileViewModel)))
                {
                    vm = Locator.CurrentMutable.GetService<ProfileViewModel>();
                }
                else if (urlPathSegment.Equals(nameof(WelcomeViewModel)))
                {
                    vm = Locator.CurrentMutable.GetService<WelcomeViewModel>();
                }
                else
                {
                    return;
                }

                this.m_VModelDict.Add(urlPathSegment, vm);
            }

            SubRouter.Navigate.Execute(vm);
        }

    }
}
