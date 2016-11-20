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

using ajayumi.AiXin.Model;
using FluentMigrator;
using System;

namespace ajayumi.AiXin.Data.Migrator
{
    [Migration(1001)]
    public class DataBaseScript_1001 : AutoReversingMigration
    {
        private const string TABLE_ROLE_NAME = "tb_Role";   //角色表
        private const string TABLE_USER_NAME = "tb_User";   //用户表
        private const string TABLE_USERROLE_NAME = "tb_UserRoles";  //用户角色关系表
        private const string TABLE_GRADE_NAME = "tb_Grade"; //等级表
        private const string TABLE_CONTRACTS_NAME = "tb_Contacts";   //联系人表
        private const string TABLE_MESSAGE_NAME = "tb_Message";   //消息表
        private const string TABLE_USER_LOGIN_NAME = "tb_UserLogin";   //用户登录表

        public override void Up()
        {
            Create.Table(TABLE_ROLE_NAME)
                .WithColumn(nameof(Role.Id)).AsInt64().PrimaryKey().Identity()
                .WithColumn(nameof(Role.Name)).AsString(128).Indexed()
                .WithColumn(nameof(Role.DisplayName)).AsString(256).Nullable()
                .WithColumn(nameof(Role.CreationDTime)).AsDateTime()
                .WithColumn(nameof(Role.Remark)).AsString(512).Nullable();
            IfDatabase("mysql").Alter.Table(TABLE_ROLE_NAME).AlterColumn(nameof(Role.CreationDTime)).AsDateTime().WithDefaultValue(SystemMethods.CurrentDateTime);

            Create.Table(TABLE_USERROLE_NAME)
                .WithColumn("UserId").AsInt64().NotNullable()
                .WithColumn("RoleId").AsInt64().NotNullable();

            Create.Table(TABLE_USER_NAME)
                .WithColumn(nameof(User.Id)).AsInt64().PrimaryKey().Identity()
                .WithColumn(nameof(User.Name)).AsString(128).Indexed()
                .WithColumn(nameof(User.Email)).AsString(128).Indexed()
                .WithColumn(nameof(User.Telphone)).AsString(128).Indexed().Nullable()
                .WithColumn(nameof(User.Password)).AsString(512).Nullable()
                .WithColumn(nameof(User.NickName)).AsString(128).Nullable()
                .WithColumn(nameof(User.RealName)).AsString(128).Nullable()
                .WithColumn(nameof(User.Avatar)).AsString(512).Nullable()
                .WithColumn(nameof(User.Gender)).AsInt16().Nullable().WithDefaultValue(0)
                .WithColumn(nameof(User.Birthday)).AsDateTime().Nullable()
                .WithColumn(nameof(User.QQ)).AsString(32).Nullable()
                .WithColumn(nameof(User.WeChat)).AsString(64).Nullable()
                .WithColumn(nameof(User.Address)).AsString(512).Nullable()
                .WithColumn(nameof(User.Hobby)).AsString(512).Nullable()
                .WithColumn(nameof(User.Signature)).AsString(512).Nullable()
                .WithColumn(nameof(Grade.Score)).AsInt64().Nullable().WithDefaultValue(0)
                .WithColumn(nameof(User.CreationDTime)).AsDateTime()
                .WithColumn(nameof(User.Remark)).AsString(512).Nullable();
            IfDatabase("mysql").Alter.Table(TABLE_USER_NAME).AlterColumn(nameof(User.CreationDTime)).AsDateTime().WithDefaultValue(SystemMethods.CurrentDateTime);

            Create.Table(TABLE_USER_LOGIN_NAME)
                .WithColumn(nameof(UserLogin.Id)).AsInt64().PrimaryKey().Identity()
                .WithColumn(nameof(UserLogin.UserId)).AsInt64()
                .WithColumn(nameof(UserLogin.CustomId)).AsString(128)
                .WithColumn(nameof(UserLogin.IP)).AsString(128)
                .WithColumn(nameof(UserLogin.ConnectState)).AsInt16()
                .WithColumn(nameof(UserLogin.CreationDTime)).AsDateTime()
                .WithColumn(nameof(UserLogin.Remark)).AsString(512).Nullable();
            IfDatabase("mysql").Alter.Table(TABLE_USER_LOGIN_NAME).AlterColumn(nameof(UserLogin.CreationDTime)).AsDateTime().WithDefaultValue(SystemMethods.CurrentDateTime);

            Create.Table(TABLE_GRADE_NAME)
                .WithColumn(nameof(Grade.Id)).AsInt64().PrimaryKey().Identity()
                .WithColumn(nameof(Grade.Name)).AsString(128).Indexed()
                .WithColumn(nameof(Grade.Score)).AsInt64()
                .WithColumn(nameof(Grade.CreationDTime)).AsDateTime()
                .WithColumn(nameof(Grade.Remark)).AsString(512).Nullable();
            IfDatabase("mysql").Alter.Table(TABLE_GRADE_NAME).AlterColumn(nameof(Grade.CreationDTime)).AsDateTime().WithDefaultValue(SystemMethods.CurrentDateTime);

            Create.Table(TABLE_CONTRACTS_NAME)
                .WithColumn(nameof(Contacts.Id)).AsInt64().PrimaryKey().Identity()
                .WithColumn("OUserId").AsInt64().NotNullable()
                .WithColumn("CUserId").AsInt64().NotNullable()
                .WithColumn(nameof(Contacts.CustomName)).AsString(128).Nullable()
                .WithColumn(nameof(Contacts.CreationDTime)).AsDateTime();
            IfDatabase("mysql").Alter.Table(TABLE_CONTRACTS_NAME).AlterColumn(nameof(Contacts.CreationDTime)).AsDateTime().WithDefaultValue(SystemMethods.CurrentDateTime);

            Create.Table(TABLE_MESSAGE_NAME)
                .WithColumn(nameof(Message.Id)).AsInt64().PrimaryKey().Identity()
                .WithColumn(nameof(Message.FromUserId)).AsInt64().NotNullable()
                .WithColumn(nameof(Message.ToUserId)).AsInt64().NotNullable()
                .WithColumn(nameof(Message.Content)).AsString().Nullable()
                .WithColumn(nameof(Message.CreationDTime)).AsDateTime();
            IfDatabase("mysql").Alter.Table(TABLE_CONTRACTS_NAME).AlterColumn(nameof(Message.CreationDTime)).AsDateTime().WithDefaultValue(SystemMethods.CurrentDateTime);

            //初始化数据
            Insert.IntoTable(TABLE_ROLE_NAME).Row(new { Name = "Administrators", DisplayName = "超级管理员", CreationDTime = DateTime.Now });
            Insert.IntoTable(TABLE_ROLE_NAME).Row(new { Name = "Managers", DisplayName = "管理员", CreationDTime = DateTime.Now });
            Insert.IntoTable(TABLE_ROLE_NAME).Row(new { Name = "Vagrants", DisplayName = "流浪者", CreationDTime = DateTime.Now });
            Insert.IntoTable(TABLE_USER_NAME).Row(new
            {
                //CustomId = Guid.NewGuid().ToString(),
                Name = "ajayumi",
                Email = "aj-ayumi@163.com",
                Telphone = "13584089910",
                Password = "9CO+MYhhNikrNjRTFUPk2IsGU42cVz3/OlfrKJwz5O3UfHdho2xt2Aw7+XTEeNykk0WsioKao9PeWLap6E828A==",
                NickName = "小瓜瓜",
                RealName = "黄小冬",
                Birthday = DateTime.Parse("1985-01-01"),
                QQ = "496230094",
                WeChat = "ajayumi",
                Address = "江苏省南京市",
                Hobby = "音乐,写字",
                Signature = "无个性，不签名……",
                //Avatar = "pack://application:,,,/Images/user_head.jpg",
                Score = 999999999999,
                CreationDTime = DateTime.Now
            });
            Insert.IntoTable(TABLE_USERROLE_NAME).Row(new { UserId = 1, RoleId = 1 });
            for (int i = 10; i > 0; i--)
            {
                Insert.IntoTable(TABLE_GRADE_NAME).Row(new { Name = $"VIP{i}", Score = long.Parse(("10000000000000").Substring(0, (i % 11) + 2)), CreationDTime = DateTime.Now });
            }

        }
    }
}
