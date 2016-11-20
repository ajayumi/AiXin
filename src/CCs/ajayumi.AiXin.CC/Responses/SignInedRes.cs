using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajayumi.AiXin.CC.Responses
{
    /// <summary>
    /// 登录返回值
    /// </summary>
    public class SignInedRes : ResBase
    {
        public string UniqueId { get; set; }

        public string UserName { get; set; }

        public override string ToString()
        {
            return $"{nameof(UniqueId)}: {UniqueId}, {nameof(UserName)}: {UserName}, {nameof(CreationDTime)}: {CreationDTime.ToString("yyyy-MM-dd HH:mm:ss")}";
        }
    }
}
