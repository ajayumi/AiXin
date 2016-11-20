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

using ajayumi.AiXin.App.Wpf.ViewModels;
using ajayumi.AiXin.App.Wpf.Views;
using ajayumi.AiXin.Infrastructure;
using MahApps.Metro.Controls.Dialogs;
using ReactiveUI;
using Splat;

namespace ajayumi.AiXin.App.Wpf
{
    public class AppBootstrapper : ReactiveObject, IScreen
    {
        public RoutingState Router { get; private set; }

        private MainViewModel m_MainVM;
        public MainViewModel MainVM
        {
            get { return m_MainVM; }
            set { this.RaiseAndSetIfChanged(ref m_MainVM, value); }
        }

        public AppBootstrapper(IMutableDependencyResolver dependencyResolver = null, RoutingState router = null)
        {
            Router = router ?? new RoutingState();
            dependencyResolver = dependencyResolver ?? Locator.CurrentMutable;

            // Bind 
            RegisterParts(dependencyResolver);

            // TODO: This is a good place to set up any other app 
            // startup tasks, like setting the logging level
            LogHost.Default.Level = LogLevel.Debug;

            // Navigate to the opening page of the application
            Router.Navigate.Execute(Locator.CurrentMutable.GetService<SignInViewModel>());

            this.MainVM = new MainViewModel(this);
        }

        private void RegisterParts(IMutableDependencyResolver dependencyResolver)
        {
            dependencyResolver.RegisterConstant(this, typeof(IScreen));

            dependencyResolver.Register(() => new SignInViewModel(this), typeof(SignInViewModel));
            dependencyResolver.Register(() => new SignInView(), typeof(IViewFor<SignInViewModel>));

            dependencyResolver.Register(() => new SignUpViewModel(this), typeof(SignUpViewModel));
            dependencyResolver.Register(() => new SignUpView(), typeof(IViewFor<SignUpViewModel>));

            dependencyResolver.Register(() => new WelcomeViewModel(this), typeof(WelcomeViewModel));
            dependencyResolver.Register(() => new WelcomeView(), typeof(IViewFor<WelcomeViewModel>));

            dependencyResolver.Register(() => new HomeViewModel(this), typeof(HomeViewModel));
            dependencyResolver.Register(() => new HomeView(), typeof(IViewFor<HomeViewModel>));

            dependencyResolver.Register(() => new ChatViewModel(this), typeof(ChatViewModel));
            dependencyResolver.Register(() => new ChatView(), typeof(IViewFor<ChatViewModel>));

            dependencyResolver.Register(() => new GroupViewModel(this), typeof(GroupViewModel));
            dependencyResolver.Register(() => new GroupView(), typeof(IViewFor<GroupViewModel>));

            dependencyResolver.Register(() => new ContactsViewModel(this), typeof(ContactsViewModel));
            dependencyResolver.Register(() => new ContractView(), typeof(IViewFor<ContactsViewModel>));

            dependencyResolver.Register(() => new SearchViewModel(this), typeof(SearchViewModel));
            dependencyResolver.Register(() => new SearchView(), typeof(IViewFor<SearchViewModel>));

            dependencyResolver.Register(() => new SettingViewModel(this, DialogCoordinator.Instance), typeof(SettingViewModel));
            dependencyResolver.Register(() => new SettingView(), typeof(IViewFor<SettingViewModel>));

            dependencyResolver.Register(() => new ProfileViewModel(this), typeof(ProfileViewModel));
            dependencyResolver.Register(() => new ProfileView(), typeof(IViewFor<ProfileViewModel>));

            dependencyResolver.Register(() => new WaitingPanelViewModel(), typeof(WaitingPanelViewModel));
            //dependencyResolver.Register(() => new ChatDetailViewModel(), typeof(ChatDetailViewModel));

            switch (AppConfiger.AppCfg.CurrCCCfgMode)
            {
                case CC_Config.EMode.SuperSocket:
                default:
                    dependencyResolver.Register(() => new CC.SuperSocket.Communication(), typeof(CC.ICommunication));
                    break;
            }

        }
    }
}
