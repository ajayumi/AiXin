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
using ajayumi.AiXin.App.Wpf.Utils;
using ajayumi.AiXin.Infrastructure;
using ajayumi.AiXin.Model;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ajayumi.AiXin.App.Wpf.ViewModels
{
    public interface IContactsViewModel : IRoutableViewModel
    {
        /// <summary>
        /// 搜索
        /// </summary>
        IReactiveCommand SearchCommand { get; }
    }

    public class ContactsViewModel : ReactiveObject, IContactsViewModel
    {
        public IScreen HostScreen { get; protected set; }

        public string UrlPathSegment
        {
            get { return nameof(ContactsViewModel); }
        }

        IReactiveCommand IContactsViewModel.SearchCommand
        {
            get { return SearchCommand; }
        }
        public ReactiveCommand<object> SearchCommand { get; set; }

        public ReactiveList<ContactsUser> Contracts { get; set; }

        public ContactsViewModel() { }

        public ContactsViewModel(IScreen screen)
        {
            HostScreen = screen;

            this.Contracts = new ReactiveList<ContactsUser>();
            Loginer.CurrCC.GetContactsCallbackHandler = this.GetContactsCallback;
            Loginer.CurrCC.DownloadAvatarCallbackHandler = this.DownloadAvatarCallback;

            this.SearchCommand = ReactiveCommand.Create();

            this.SearchCommand.Subscribe(o =>
            {
                string account = string.Empty;
                if (o is TextBox)
                {
                    TextBox txt = (TextBox)o;
                    account = txt.Text;
                    txt.Text = string.Empty;
                }
                else
                {
                    account = o.ToString();
                }

                if (Loginer.CurrCC.IsConnected)
                {
                    Loginer.CurrCC.GetContacts(Loginer.CurrUser.Id, account, 1, 10);
                }
            });

            this.SearchCommand.Execute(string.Empty);
        }

        private void GetContactsCallback(ResultInfo<IEnumerable<Contacts>> res)
        {
            this.Clear();

            if (!res.Success)
            {
                MessageBox.Show(res.RMsg, AppConfiger.AppCfg.AppName, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Observable.Start(() =>
            {
                foreach (var item in res.RData)
                {
                    Contracts.Add(new ContactsUser() { User = item.CUser, CustomName = item.CustomName });
                    Loginer.CurrCC.DownloadAvatar(item.CUser.Id);
                }
            }, RxApp.MainThreadScheduler);
        }

        private void DownloadAvatarCallback(ResultInfo<Tuple<long, byte[]>> res)
        {
            if (!res.Success)
            {
                return;
            }

            long userId = res.RData.Item1;
            byte[] data = res.RData.Item2;

            var item = this.Contracts.FirstOrDefault(o => o.User.Id == userId);
            if (item == null)
            {
                return;
            }
            var user = item.User;

            try
            {
                item.Avatar = FileHelper.SaveAvatar(data, user.Avatar);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, AppConfiger.AppCfg.AppName, MessageBoxButton.OK, MessageBoxImage.Error);
            }

            //string avatarPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, user.Avatar);
            ////判断文件是否存在
            //if (File.Exists(avatarPath))
            //{
            //    FileInfo file = new FileInfo(avatarPath);
            //    if (file.Length == data.Length)
            //    {
            //        item.Avatar = avatarPath;
            //        return;
            //    }
            //}

            //try
            //{
            //    string dir = Path.GetDirectoryName(avatarPath);
            //    if (!Directory.Exists(dir))
            //    {
            //        Directory.CreateDirectory(dir);
            //    }
            //    using (FileStream fs = new FileStream(avatarPath, FileMode.OpenOrCreate))
            //    {
            //        fs.Write(data, 0, data.Length);
            //    }

            //    item.Avatar = avatarPath;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, AppConfiger.AppCfg.AppName, MessageBoxButton.OK, MessageBoxImage.Error);
            //}
        }

        private void Clear()
        {
            Observable.Start(() => this.Contracts.Clear(), RxApp.MainThreadScheduler);
        }
    }
}
