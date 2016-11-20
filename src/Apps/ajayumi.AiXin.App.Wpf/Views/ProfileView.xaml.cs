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
using ReactiveUI;
using System.Windows;
using System.Windows.Controls;

namespace ajayumi.AiXin.App.Wpf.Views
{
    /// <summary>
    /// ProfileView.xaml 的交互逻辑
    /// </summary>
    public partial class ProfileView : UserControl, IViewFor<IProfileViewModel>
    {
        public IProfileViewModel ViewModel
        {
            get { return (IProfileViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register("ViewModel", typeof(IProfileViewModel), typeof(ProfileView), new PropertyMetadata(null));

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (IProfileViewModel)value; }
        }

        public ProfileView()
        {
            InitializeComponent();
            this.WhenAnyValue(x => x.ViewModel).BindTo(this, x => x.DataContext);
        }
    }
}
