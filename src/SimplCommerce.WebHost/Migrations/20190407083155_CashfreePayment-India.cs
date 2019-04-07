using Microsoft.EntityFrameworkCore.Migrations;

namespace SimplCommerce.WebHost.Migrations
{
    public partial class CashfreePaymentIndia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Localization_Culture",
                keyColumn: "Id",
                keyValue: "en-US");

            migrationBuilder.InsertData(
                table: "Localization_Culture",
                columns: new[] { "Id", "Name" },
                values: new object[] { "en-IN", "English (IN)" });

            migrationBuilder.InsertData(
                table: "Payments_PaymentProvider",
                columns: new[] { "Id", "AdditionalSettings", "ConfigureUrl", "IsEnabled", "LandingViewComponentName", "Name" },
                values: new object[] { "Cashfree", "{ \"IsSandbox\":true, \"AppId\":\"358035b02486f36ca27904540853\", \"SecretKey\":\"26f48dcd6a27f89f59f28e65849e587916dd57b9\" }", "payments-cashfree-config", true, "CashfreeLanding", "Cashfree Payment Gateway" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Localization_Culture",
                keyColumn: "Id",
                keyValue: "en-IN");

            migrationBuilder.DeleteData(
                table: "Payments_PaymentProvider",
                keyColumn: "Id",
                keyValue: "Cashfree");

            migrationBuilder.InsertData(
                table: "Localization_Culture",
                columns: new[] { "Id", "Name" },
                values: new object[] { "en-US", "English (US)" });
        }
    }
}
