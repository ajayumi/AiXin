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
    /// 对话请求
    /// </summary>
    public class TalkReq
    {
        /// <summary>
        /// 源用户Id
        /// </summary>
        public string FromUId { get; set; }
        /// <summary>
        /// 目标用户Id
        /// </summary>
        public string ToUId { get; set; }
        /// <summary>
        /// 对话内容
        /// </summary>
        public string Content { get; set; }
    }
}
