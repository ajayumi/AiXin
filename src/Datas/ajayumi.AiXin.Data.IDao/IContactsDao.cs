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

using ajayumi.AiXin.Model;

namespace ajayumi.AiXin.Data.IDao
{
    public interface IContactsDao : IDaoBase<Contacts>
    {
        Contacts FindById(long oUserId, long cUserId);

        bool Exists(long oUserId, long cUserId);
    }
}
