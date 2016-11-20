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

using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Text;

namespace ajayumi.AiXin.Infrastructure.Json
{
    public class JsonDictCollection : List<JsonDict>
    {
        public byte[] ToBytes()
        {
            return Encoding.UTF8.GetBytes(this.ToString());
        }


        public override string ToString()
        {
            return $"[{string.Join(",", this)}]";
        }

        public static JsonDictCollection Parse(string jArgsStr)
        {
            JsonDictCollection collection = new JsonDictCollection();
            JArray array = JArray.Parse(jArgsStr);
            foreach (var item in array)
            {
                collection.Add(new JsonDict((JObject)item));
            }

            return collection;

        }
    }
}
