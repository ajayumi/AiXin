using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajayumi.AiXin.CC.Responses
{
    /// <summary>
    /// 连接服务器响应
    /// </summary>
    public class ConnectedRes : ResBase
    {
        public string UniqueId { get; set; }

        public override string ToString()
        {
            return $"{nameof(UniqueId)}: {UniqueId}, {nameof(CreationDTime)}: {CreationDTime.ToString("yyyy-MM-dd HH:mm:ss")}";
        }
    }
}
