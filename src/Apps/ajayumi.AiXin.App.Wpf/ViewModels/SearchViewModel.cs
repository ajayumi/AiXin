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
    public interface ISearchViewModel : IRoutableViewModel
    {
        /// <summary>
        /// 搜索
        /// </summary>
        IReactiveCommand SearchCommand { get; }
    }

    public class SearchViewModel : ReactiveObject, ISearchViewModel
    {
        public IScreen HostScreen { get; protected set; }

        public string UrlPathSegment
        {
            get { return nameof(SearchViewModel); }
        }

        IReactiveCommand ISearchViewModel.SearchCommand
        {
            get { return SearchCommand; }
        }
        public ReactiveCommand<object> SearchCommand { get; set; }

        public ReactiveList<SearchUser> m_Users;
        public IReadOnlyReactiveList<SearchUser> Users => m_Users;

        public SearchViewModel() { }

        public SearchViewModel(IScreen screen)
        {
            HostScreen = screen;

            this.m_Users = new ReactiveList<SearchUser>();
            Loginer.CurrCC.SearchUserCallbackHandler = this.SearchUserCallback;
            Loginer.CurrCC.SaveContactsCallbackHandler = this.SaveContactsCallback;
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
                    Loginer.CurrCC.SearchUser(account);
                }
            });
        }

        private void SaveContactsCallback(ResultInfo res)
        {
            if (!res.Success)
            {
                MessageBox.Show(res.RMsg, AppConfiger.AppCfg.AppName, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            this.Clear();
        }

        private void SearchUserCallback(ResultInfo<IEnumerable<User>> res)
        {
            this.Clear();
            if (!res.Success)
            {
                MessageBox.Show(res.RMsg, AppConfiger.AppCfg.AppName, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Observable.Start(() =>
            {
                this.m_Users.AddRange(res.RData.Select(o =>
                {
                    Loginer.CurrCC.DownloadAvatar(o.Id);
                    return new SearchUser() { User = o };
                }));
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

            var item = this.m_Users.FirstOrDefault(o => o.User.Id == userId);
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
            Observable.Start(() => this.m_Users.Clear(), RxApp.MainThreadScheduler);
        }

    }
}
