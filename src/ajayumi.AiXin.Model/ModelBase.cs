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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajayumi.AiXin.Model
{
    public class ModelBase
    {
        public long Id { get; set; }
        /// <summary>
        /// 创建日期（含时间）
        /// </summary>
        public DateTime CreationDTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

    }
}
