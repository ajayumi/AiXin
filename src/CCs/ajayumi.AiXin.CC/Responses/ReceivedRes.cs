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


namespace ajayumi.AiXin.CC.Responses
{
    public class ReceivedRes : ResBase
    {
        /// <summary>
        /// 发送者
        /// </summary>
        public string FromUser { get; set; }

        /// <summary>
        /// 接收者
        /// </summary>
        public string ToUser { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }

        public override string ToString()
        {
            return $"{nameof(FromUser)}: {FromUser}, {nameof(ToUser)}: {ToUser}, {nameof(Content)}: {Content}, {nameof(CreationDTime)}: {CreationDTime.ToString("yyyy-MM-dd HH:mm:ss")}";
        }
    }
}