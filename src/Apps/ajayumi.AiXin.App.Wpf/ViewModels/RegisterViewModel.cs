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

using MahApps.Metro.Controls.Dialogs;
using ReactiveUI;
using System;
using System.Threading.Tasks;

namespace ajayumi.AiXin.App.Wpf.ViewModels
{
    public class RegisterViewModel : ReactiveObject
    {
        private readonly IDialogCoordinator _dialogCoordinator;

        public ReactiveCommand<bool> ExecuteRegisterAccount { get; protected set; }

        private string m_UserName;

        public string UserName
        {
            get { return m_UserName; }
            set { this.RaiseAndSetIfChanged(ref this.m_UserName, value); }
        }

        public ReactiveCommand<object> SubmitCommand { get; protected set; }
        public ReactiveCommand<object> CancelCommand { get; protected set; }

        public RegisterViewModel()
        {
            ExecuteRegisterAccount = ReactiveCommand.CreateAsyncTask(paramter => RegisterAccount(paramter));

            this.SubmitCommand = ReactiveCommand.Create(this.WhenAny(x => x.UserName, x => !string.IsNullOrEmpty(x.Value)));
            this.SubmitCommand.Subscribe(paramter =>
            {
                ExecuteRegisterAccount.Execute(paramter);
            });

        }

        public RegisterViewModel(IDialogCoordinator dialogCoordinator)
            : this()
        {
            _dialogCoordinator = dialogCoordinator;
        }

        private async Task<bool> RegisterAccount(object state)
        {
            return await Task.FromResult<bool>(true);
        }

    }
}
