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
    /// 注册请求
    /// </summary>
    public class SignUpReq
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Pwd { get; set; }
    }
}
