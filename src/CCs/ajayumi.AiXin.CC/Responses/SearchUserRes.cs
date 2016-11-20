using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajayumi.AiXin.CC.Responses
{
    public class SearchUserRes : ResBase
    {
        public long Id { get; set; }

        /// <summary>
        /// 用户名(登录帐号)
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 电子邮箱(登录帐号)
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 手机号码(登录帐号)
        /// </summary>
        public string Telphone { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 个性签名
        /// </summary>
        public string Signature { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public EGender Gender { get; set; }

    }
}
