using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajayumi.AiXin.CC.Responses
{
    /// <summary>
    /// 获取在线用户列表响应对象
    /// </summary>
    public class GetOnlineListRes : ResBase
    {
        public IEnumerable<OnlineUser> Users { get; set; }

        //public override string ToString()
        //{
        //    return $"{nameof(UniqueId)}: {UniqueId}, {nameof(UserName)}: {UserName}, {nameof(CreationDTime)}: {CreationDTime.ToString("yyyy-MM-dd HH:mm:ss")}";
        //}

        public class OnlineUser
        {
            public string UniqueId { get; set; }

            public string Name { get; set; }

            public string NickName { get; set; }

            public string CustomName { get; set; }

        }
    }
}
