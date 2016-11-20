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

using System;

namespace ajayumi.AiXin.Infrastructure
{
    public class ResultInfo
    {
        public string RCode { get; set; }

        public string RMsg { get; set; }

        public bool Success { get; set; }

        public ResultInfo()
        {
            this.RCode = "0000";
            this.RMsg = "执行成功";
            this.Success = true;

        }

        public ResultInfo(Exception ex)
        {
            this.RCode = "9999";
            this.RMsg = ex.Message;
            this.Success = false;
        }
    }

    public class ResultInfo<T> : ResultInfo
    {
        public T RData { get; set; }

        public ResultInfo()
            : base()
        {
        }

        public ResultInfo(Exception ex)
            : base(ex)
        {
        }
    }

    public class ListResultInfo<T> : ResultInfo<T> where T : System.Collections.IEnumerable
    {
        public long TotalRecords { get; set; }

        public ListResultInfo()
            : base()
        {
        }

        public ListResultInfo(Exception ex)
            : base(ex)
        {
        }
    }
}
