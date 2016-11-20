﻿#region Copyright
//************************************************************************************/
//	创建人员(Creator)    ：ajayumi
//	创建日期(Date)       ：2016-11-20
//  联系方式(Email)      ：aj-ayumi@163.com; gajayumi@gmail.com;
//  描    述(Description)：
//
//	Copyright(C) 2009-2016 ajayumi.All rights reserved.
//************************************************************************************/
#endregion


namespace ajayumi.AiXin.Data
{
    public class CriteriaPage : CriteriaBase
    {
        public int PageNum { get; set; }
        public int PageSize { get; set; }

        public int PageIndex
        {
            get { return (PageNum - 1) * PageSize; }
        }
    }
}
