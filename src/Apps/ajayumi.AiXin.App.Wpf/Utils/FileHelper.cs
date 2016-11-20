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
using System.IO;

namespace ajayumi.AiXin.App.Wpf.Utils
{
    /// <summary>
    /// 文件辅助类
    /// </summary>
    internal class FileHelper
    {
        /// <summary>
        /// 保存用户头像
        /// </summary>
        internal static string SaveAvatar(byte[] data, string avatarFileName)
        {
            string avatarPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, avatarFileName);
            //判断文件是否存在
            if (File.Exists(avatarPath))
            {
                FileInfo file = new FileInfo(avatarPath);
                if (file.Length == data.Length)
                {
                    return avatarPath;
                }
            }

            try
            {
                string dir = Path.GetDirectoryName(avatarPath);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                using (FileStream fs = new FileStream(avatarPath, FileMode.OpenOrCreate))
                {
                    fs.Write(data, 0, data.Length);
                }

                return avatarPath;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
