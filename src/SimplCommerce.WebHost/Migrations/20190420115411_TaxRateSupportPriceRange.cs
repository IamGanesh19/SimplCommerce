using Microsoft.EntityFrameworkCore.Migrations;

namespace SimplCommerce.WebHost.Migrations
{
    public partial class TaxRateSupportPriceRange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "MaxPriceRange",
                table: "Tax_TaxRate",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MinPriceRange",
                table: "Tax_TaxRate",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxPriceRange",
                table: "Tax_TaxRate");

            migrationBuilder.DropColumn(
                name: "MinPriceRange",
                table: "Tax_TaxRate");
        }
    }
}
