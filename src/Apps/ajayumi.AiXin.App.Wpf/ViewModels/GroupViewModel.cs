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

using ReactiveUI;

namespace ajayumi.AiXin.App.Wpf.ViewModels
{
    public interface IGroupViewModel : IRoutableViewModel
    {

    }

    public class GroupViewModel : ReactiveObject, IGroupViewModel
    {
        public IScreen HostScreen { get; protected set; }

        public string UrlPathSegment
        {
            get { return nameof(GroupViewModel); }
        }

        public GroupViewModel() { }

        public GroupViewModel(IScreen screen)
        {
            HostScreen = screen;
        }
    }
}
