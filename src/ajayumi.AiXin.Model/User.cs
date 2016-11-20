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
    /// 用户
    /// </summary>
    public class User : ModelBase
    {
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
        /// 密码(登录密码)
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }
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
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime Birthday { get; set; }
        /// <summary>
        /// QQ号码
        /// </summary>
        public string QQ { get; set; }
        /// <summary>
        /// 微信号
        /// </summary>
        public string WeChat { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 嗜好
        /// </summary>
        public string Hobby { get; set; }
        /// <summary>
        /// 用户等级
        /// </summary>
        public Grade UserGrade { get; set; }
        /// <summary>
        /// 连接状态
        /// </summary>
        public EConnectState ConnectState { get; set; }

    }
}
