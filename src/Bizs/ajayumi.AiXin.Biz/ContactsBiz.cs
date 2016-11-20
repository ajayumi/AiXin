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

using ajayumi.AiXin.Data;
using ajayumi.AiXin.Data.IDao;
using ajayumi.AiXin.IBiz;
using ajayumi.AiXin.Infrastructure;
using ajayumi.AiXin.Model;
using System;
using System.Collections.Generic;

namespace ajayumi.AiXin.Biz
{
    public class ContactsBiz : IContactsBiz
    {
        private readonly IContactsDao m_Dao;

        public ContactsBiz(IContactsDao dao)
        {
            this.m_Dao = dao;
        }

        public ResultInfo<IEnumerable<Contacts>> GetDatas(long oUserId, string cUserName, string cNickName, string cTelphone, string cEmail, string customName, int pageNum, int pageSize)
        {
            ResultInfo<IEnumerable<Contacts>> result = null;
            try
            {
                var model = new ContactsGetDataPagerSqlCrieria() { OUserId = oUserId, CEmail = cEmail, CNickName = cNickName, CTelphone = cTelphone, CUserName = cUserName, CustomName = customName, PageNum = pageNum, PageSize = pageSize };
                var datas = this.m_Dao.GetDataPager(model);
                result = new ResultInfo<IEnumerable<Contacts>>() { RMsg = "获取通讯录列表成功", RData = datas };
            }
            catch (Exception ex)
            {
                result = new ResultInfo<IEnumerable<Contacts>>(ex);
            }
            return result;
        }

        public ResultInfo Save(long oUserId, long cUserId, string customName)
        {
            ResultInfo result = null;
            try
            {
                if (oUserId == cUserId)
                {
                    return new ResultInfo() { RCode = "1001", RMsg = "不能自己添加自己", Success = false };
                }

                result = new ResultInfo();
                bool exists = this.m_Dao.Exists(oUserId, cUserId);
                if (!exists)
                {
                    this.m_Dao.Add(new Contacts()
                    {
                        OUser = new User() { Id = oUserId },
                        CUser = new User() { Id = cUserId }
                    });
                    result.RMsg = "通讯录新增成功";
                }
                else
                {
                    var contacts = this.m_Dao.FindById(oUserId, cUserId);
                    if (!string.IsNullOrEmpty(customName))
                    {
                        contacts.CustomName = customName;
                    }
                    this.m_Dao.Update(contacts);
                    result.RMsg = "通讯录修改成功";
                }

            }
            catch (Exception ex)
            {
                result = new ResultInfo(ex);
            }
            return result;
        }


    }
}
