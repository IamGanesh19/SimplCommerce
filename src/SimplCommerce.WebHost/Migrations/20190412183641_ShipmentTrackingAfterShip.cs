using Microsoft.EntityFrameworkCore.Migrations;

namespace SimplCommerce.WebHost.Migrations
{
    public partial class ShipmentTrackingAfterShip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Core_AppSetting",
                columns: new[] { "Id", "IsVisibleInCommonSettingPage", "Module", "Value" },
                values: new object[] { "Aftership.ApiKey", false, "ShipmentTrackingAfterShip", "" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Core_AppSetting",
                keyColumn: "Id",
                keyValue: "Aftership.ApiKey");
        }
    }
}
