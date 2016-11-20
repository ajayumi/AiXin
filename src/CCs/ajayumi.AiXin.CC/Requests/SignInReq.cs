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


namespace ajayumi.AiXin.CC.Requests
{
    /// <summary>
    /// 登陆请求
    /// </summary>
    public class SignInReq
    {
        /// <summary>
        /// 登陆账号（含用户名、电子邮箱、手机号）
        /// </summary>
        public string Account { get; set; }
        public string Pwd { get; set; }
    }
}
