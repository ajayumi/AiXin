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
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ajayumi.AiXin.Infrastructure.Json
{
    /// <summary>
    /// 请求者
    /// </summary>
    public class JsonDict : IDictionary<string, string>
    {
        JObject m_Json = null;

        public JsonDict()
            : this(new JObject())
        {

        }

        public JsonDict(JObject other)
        {
            m_Json = other;
        }

        //public JsonDict(string oper_companycode, string oper_companyname, string oper_usercode, string oper_username, string oper_ip)
        //{
        //    this.m_Json = new JObject();
        //    this.Add("oper_usercode", oper_usercode);
        //    this.Add("oper_username", oper_username);
        //    this.Add("oper_ip", oper_ip);
        //}

        public override string ToString()
        {
            //string oper_ip = string.Empty;
            //if (this.TryGetValue("oper_ip", out oper_ip))
            //{
            //    if (string.IsNullOrEmpty(oper_ip))
            //    {
            //        StringBuilder sb = new StringBuilder();
            //        IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            //        foreach (IPAddress ip in host.AddressList)
            //        {
            //            if (ip.AddressFamily.ToString() == "InterNetwork")
            //            {
            //                sb.AppendFormat("{0},", ip.ToString());
            //            }
            //        }
            //        oper_ip = sb.ToString();
            //        this.Add("oper_ip", oper_ip);
            //    }
            //}

            return m_Json.ToString(Formatting.None);
        }

        public byte[] ToBytes()
        {
            return Encoding.UTF8.GetBytes(this.ToString());
        }

        public void Add(string key, string val)
        {
            if (this.ContainsKey(key))
            {
                this[key] = val;
            }
            else
            {
                this.m_Json.Add(key, val);
            }
        }

        public void Add(string key, JsonDict reqObj)
        {
            if (this.ContainsKey(key))
            {
                this[key] = reqObj.ToString();
            }
            else
            {
                JObject obj = JObject.Parse(reqObj.ToString());
                this.m_Json.Add(key, obj);
            }
        }

        public void AddInt(string key, int val)
        {
            if (this.ContainsKey(key))
            {
                throw new ArgumentException("不允许重复添加键值");
            }
            else
            {
                this.m_Json.Add(key, val);
            }
        }

        public void AddLong(string key, long val)
        {
            if (this.ContainsKey(key))
            {
                throw new ArgumentException("不允许重复添加键值");
            }
            else
            {
                this.m_Json.Add(key, val);
            }
        }

        public void AddDouble(string key, double val)
        {
            if (this.ContainsKey(key))
            {
                throw new ArgumentException("不允许重复添加键值");
            }
            else
            {
                this.m_Json.Add(key, val);
            }
        }

        public static JsonDict Parse(string jArgsStr)
        {
            JObject obj = JObject.Parse(jArgsStr);
            JsonDict r = new JsonDict(obj);

            return r;
        }


        public bool ContainsKey(string key)
        {
            object obj = this.m_Json[key];
            return obj != null;
        }

        public ICollection<string> Keys
        {
            get
            {
                return this.m_Json.Properties().Select(p => p.Name).ToList();
            }
        }

        public bool Remove(string key)
        {
            return this.m_Json.Remove(key);
        }

        public bool TryGetValue(string key, out string value)
        {
            JToken token = null;
            bool result = this.m_Json.TryGetValue(key, out token);
            if (result)
            {
                value = token.ToString();
            }
            else
            {
                value = string.Empty;
            }
            return result;
        }

        public ICollection<string> Values
        {
            get
            {
                return new List<string>(this.m_Json.Values<string>());
            }
        }

        public string this[string key]
        {
            get
            {
                JToken token = this.m_Json[key];
                if (token != null)
                {
                    return token.ToString();
                }
                return string.Empty;
            }
            set
            {
                this.m_Json[key] = value;
            }
        }

        public void Add(KeyValuePair<string, string> item)
        {
            this.m_Json.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            this.m_Json.RemoveAll();
        }

        public bool Contains(KeyValuePair<string, string> item)
        {
            string str = this[item.Key];
            if (!string.IsNullOrEmpty(str))
            {
                return str == item.Value;
            }
            return false;
        }

        public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return this.m_Json.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(KeyValuePair<string, string> item)
        {
            return this.m_Json.Remove(item.Key);
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            List<KeyValuePair<string, string>> lst = new List<KeyValuePair<string, string>>();
            IEnumerator<KeyValuePair<string, JToken>> enumerator = this.m_Json.GetEnumerator();
            while (enumerator.MoveNext())
            {
                lst.Add(new KeyValuePair<string, string>(enumerator.Current.Key, enumerator.Current.Value.ToString()));
            }
            return lst.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.m_Json.GetEnumerator();
        }

        //public string ToString(string format, IFormatProvider formatProvider)
        //{
        //    return string.Format(formatProvider, format, this.ToString());
        //}

        //public string ToString(IFormatProvider formatProvider)
        //{ return ToString("{0}", formatProvider); }

        /// <summary>
        /// 获取整型值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int GetInt(string key)
        {
            int result = 0;
            int.TryParse(this[key], out result);
            return result;
        }

        /// <summary>
        /// 获取长整型值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long GetLong(string key)
        {
            long result = 0L;
            long.TryParse(this[key], out result);
            return result;
        }
    }
}
