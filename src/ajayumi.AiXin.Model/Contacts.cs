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
    /// <summary>
    /// 通讯录
    /// </summary>
    public class Contacts : ModelBase
    {
        /// <summary>
        /// 通讯录所属人
        /// </summary>
        public User OUser { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public User CUser { get; set; }

        /// <summary>
        /// 联系人自定义名称
        /// </summary>
        public string CustomName { get; set; }
    }
}
