using Microsoft.EntityFrameworkCore.Migrations;

namespace SimplCommerce.WebHost.Migrations
{
    public partial class SupportSMTPFromAdress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Core_AppSetting",
                columns: new[] { "Id", "IsVisibleInCommonSettingPage", "Module", "Value" },
                values: new object[] { "SmtpFrom", false, "EmailSenderSmpt", "" });

            migrationBuilder.InsertData(
                table: "Core_AppSetting",
                columns: new[] { "Id", "IsVisibleInCommonSettingPage", "Module", "Value" },
                values: new object[] { "SmtpFromDisplayName", false, "EmailSenderSmpt", "" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Core_AppSetting",
                keyColumn: "Id",
                keyValue: "SmtpFrom");

            migrationBuilder.DeleteData(
                table: "Core_AppSetting",
                keyColumn: "Id",
                keyValue: "SmtpFromDisplayName");
        }
    }
}
