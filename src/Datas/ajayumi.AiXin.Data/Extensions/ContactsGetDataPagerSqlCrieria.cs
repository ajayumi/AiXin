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

using System.Collections.Generic;
using System.Text;

namespace ajayumi.AiXin.Data
{
    public class ContactsGetDataPagerSqlCrieria : SqlCriteria
    {
        public long OUserId { get; set; }

        public string CUserName { get; set; }

        public string CNickName { get; set; }

        public string CTelphone { get; set; }

        public string CEmail { get; set; }

        public string CustomName { get; set; }

        public override object GenSqlParams()
        {
            return new
            {
                OUserId = this.OUserId,
                UserName = this.CUserName,
                NickName = this.CNickName,
                Telphone = this.CTelphone,
                Email = this.CEmail,
                CustomName = this.CustomName,
                PageIndex = this.PageIndex,
                PageSize = this.PageSize
            };
        }

        public override string GenSqlScript()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT t.* FROM tb_Contacts t ");
            sb.Append("INNER JOIN tb_User t2 ON t2.Id = t.CUserId ");
            sb.Append("WHERE t.OUserId = @OUserId ");

            List<string> lst = new List<string>();
            if (!string.IsNullOrEmpty(this.CUserName))
            {
                lst.Add("t2.Name = @UserName");
            }

            if (!string.IsNullOrEmpty(this.CNickName))
            {
                lst.Add("t2.NickName = @NickName");
            }

            if (!string.IsNullOrEmpty(this.CTelphone))
            {
                lst.Add("t2.Telphone = @Telphone");
            }

            if (!string.IsNullOrEmpty(this.CEmail))
            {
                lst.Add("t2.Email = @Email");
            }

            if (!string.IsNullOrEmpty(this.CustomName))
            {
                lst.Add("t.CustomName = @CustomName");
            }

            if (lst.Count > 0)
            {
                sb.AppendFormat("AND ({0})", string.Join(" OR ", lst));
            }

            sb.Append("LIMIT @PageIndex, @PageSize;");
            return sb.ToString();
        }
    }
}
