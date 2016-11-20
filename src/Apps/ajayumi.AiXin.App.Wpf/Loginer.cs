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
using System;
using System.IO;

namespace ajayumi.AiXin.App.Wpf
{
    /// <summary>
    /// 登录用户
    /// </summary>
    internal struct Loginer
    {
        internal static CC.ICommunication CurrCC { get; set; }

        internal static User CurrUser { get; set; }

        internal static string CurrUserAvatar
        {
            get
            {
                string avatar = "pack://application:,,,/ajayumi.AiXin.App.Wpf;component/Images/user_head.jpg";
                if (!string.IsNullOrEmpty(CurrUser?.Avatar))
                {
                    string tmpPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, CurrUser.Avatar);
                    if (File.Exists(tmpPath))
                    {
                        avatar = tmpPath;
                    }
                }
                return avatar;
            }
        }

        /// <summary>
        /// 注销
        /// </summary>
        internal static void SignOut()
        {
            if (CurrCC != null && CurrCC.IsConnected)
            {
                CurrCC.Disconnect();
            }

            CurrUser = null;
            CurrCC = null;
        }
    }
}
