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
using Microsoft.Win32;
using ReactiveUI;
using System;
using System.IO;
using System.Windows;

namespace ajayumi.AiXin.App.Wpf.ViewModels
{
    public interface IProfileViewModel : IRoutableViewModel
    {
        IReactiveCommand ChangeAvatarCommand { get; }
        IReactiveCommand SaveCommand { get; }
    }

    public class ProfileViewModel : ReactiveObject, IProfileViewModel
    {
        public IScreen HostScreen { get; protected set; }

        public string UrlPathSegment
        {
            get { return nameof(ProfileViewModel); }
        }

        IReactiveCommand IProfileViewModel.ChangeAvatarCommand
        {
            get { return ChangeAvatarCommand; }
        }
        public ReactiveCommand<object> ChangeAvatarCommand { get; protected set; }

        IReactiveCommand IProfileViewModel.SaveCommand
        {
            get { return SaveCommand; }
        }
        public ReactiveCommand<object> SaveCommand { get; protected set; }

        public string Name
        {
            get { return Loginer.CurrUser?.Name; }
        }

        public string NickName
        {
            get { return Loginer.CurrUser?.NickName; }
            set { Loginer.CurrUser.NickName = value; }
        }

        public string RealName
        {
            get { return Loginer.CurrUser?.RealName; }
            set { Loginer.CurrUser.RealName = value; }
        }

        public string Email
        {
            get { return Loginer.CurrUser?.Email; }
            set { Loginer.CurrUser.Email = value; }
        }

        public string Telphone
        {
            get { return Loginer.CurrUser?.Telphone; }
            set { Loginer.CurrUser.Telphone = value; }
        }

        public string QQ
        {
            get { return Loginer.CurrUser?.QQ; }
            set { Loginer.CurrUser.QQ = value; }
        }

        public string WeChat
        {
            get { return Loginer.CurrUser?.WeChat; }
            set { Loginer.CurrUser.WeChat = value; }
        }

        public string Signature
        {
            get { return Loginer.CurrUser?.Signature; }
            set { Loginer.CurrUser.Signature = value; }
        }

        public string Hobby
        {
            get { return Loginer.CurrUser?.Hobby; }
            set { Loginer.CurrUser.Hobby = value; }
        }

        public string Address
        {
            get { return Loginer.CurrUser?.Address; }
            set { Loginer.CurrUser.Address = value; }
        }

        public DateTime Birthday
        {
            get { return Loginer.CurrUser != null ? Loginer.CurrUser.Birthday : DateTime.MaxValue; }
            set { Loginer.CurrUser.Birthday = value; }
        }

        public bool MaleChecked
        {
            get { return Loginer.CurrUser?.Gender == Model.EGender.Male; }
            set { Loginer.CurrUser.Gender = value ? Model.EGender.Male : Model.EGender.Female; }
        }

        public bool FemaleChecked
        {
            get { return Loginer.CurrUser?.Gender == Model.EGender.Female; }
            set { Loginer.CurrUser.Gender = value ? Model.EGender.Female : Model.EGender.Male; }
        }

        private string m_Avatar = string.Empty;
        public string Avatar
        {
            get
            {
                //m_Avatar = Loginer.CurrUserAvatar;
                return m_Avatar;
            }
            set { this.RaiseAndSetIfChanged(ref this.m_Avatar, value); }
        }

        public ProfileViewModel() { }

        public ProfileViewModel(IScreen screen)
        {
            HostScreen = screen;

            Loginer.CurrCC.EditProfileCallbackHandler = EditProfileCallback;
            Loginer.CurrCC.UploadAvatarCallbackHandler = UploadAvatarCallback;

            this.ChangeAvatarCommand = ReactiveCommand.Create();
            this.SaveCommand = ReactiveCommand.Create();

            this.ChangeAvatarCommand.Subscribe(o => this.ChangeAvatar(o));

            this.SaveCommand.Subscribe(o =>
            {
                if (Loginer.CurrCC.IsConnected)
                {
                    Loginer.CurrCC.EditProfile(Loginer.CurrUser);
                }
            });

            this.Avatar = Loginer.CurrUserAvatar;
        }

        /// <summary>
        /// 上传头像
        /// </summary>
        /// <param name="path"></param>
        private void UploadAvatar(string path)
        {
            string extName = Path.GetExtension(path);
            string dir = Path.Combine("Users", "tmp", Loginer.CurrUser.Name, DateTime.Now.ToString("yyyyMMdd"));
            string destPath = Path.Combine(dir, $"{Guid.NewGuid()}{extName}");

            try
            {
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                File.Copy(path, destPath);

                byte[] data = null;
                using (FileStream fs = new FileStream(destPath, FileMode.Open))
                {
                    data = new byte[fs.Length];
                    fs.Read(data, 0, data.Length);
                }
                Loginer.CurrCC.UploadAvatar(Loginer.CurrUser.Id, extName, data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, AppConfiger.AppCfg.AppName, MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void UploadAvatarCallback(ResultInfo res)
        {
            //if (!res.Success)
            //{
            //    MessageBox.Show(res.RMsg, AppConfiger.AppCfg.AppName, MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}

            MessageBox.Show(res.RMsg, AppConfiger.AppCfg.AppName, MessageBoxButton.OK,
                res.Success ? MessageBoxImage.Information : MessageBoxImage.Error);
        }

        private void ChangeAvatar(object obj)
        {
            OpenFileDialog dlg = new OpenFileDialog() { Title = "选择图片文件", Filter = "*.jpg|*.jpg|所有文件|*.*" };
            if (dlg.ShowDialog().GetValueOrDefault())
            {
                this.Avatar = dlg.FileName;

                //上传
                this.UploadAvatar(dlg.FileName);
            }
        }

        private void EditProfileCallback(ResultInfo res)
        {
            if (!res.Success)
            {
                MessageBox.Show(res.RMsg, AppConfiger.AppCfg.AppName, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
}
