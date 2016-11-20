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

using Newtonsoft.Json;
using System.Text;

namespace ajayumi.AiXin.Infrastructure.Json
{
    public sealed class JConverter
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serialize(object obj)
        {
            string result = JsonConvert.SerializeObject(obj);
            return result;
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] SerializeToBytes(object obj)
        {
            return Encoding.UTF8.GetBytes(Serialize(obj));
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string jsonStr)
        {
            T result = JsonConvert.DeserializeObject<T>(jsonStr);
            return result;
        }
    }
}
