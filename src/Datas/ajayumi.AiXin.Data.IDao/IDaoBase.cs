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

using System.Collections.Generic;

namespace ajayumi.AiXin.Data.IDao
{
    public interface IDaoBase<TModel, TId> where TModel : class
    {
        IEnumerable<TModel> GetList();

        IEnumerable<TModel> GetDataPager(SqlCriteria criteria);

        TModel FindById(TId id);

        TModel Add(TModel model);

        void Update(TModel model);

        void Delete(TId id);

        bool Exists(string name);

        long GetRecordCount();

        long GetRecordCount(SqlCriteria criteria);
    }

    public interface IDaoBase<TModel>
        : IDaoBase<TModel, long> where TModel : class
    { }
}
