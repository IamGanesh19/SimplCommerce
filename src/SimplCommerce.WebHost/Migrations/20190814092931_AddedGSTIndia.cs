using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SimplCommerce.WebHost.Migrations
{
    public partial class AddedGSTIndia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Catalog_Product_Tax_TaxClass_TaxClassId",
                table: "Catalog_Product");

            migrationBuilder.DropTable(
                name: "Tax_TaxRate");

            migrationBuilder.DropTable(
                name: "Tax_TaxClass");

            migrationBuilder.CreateTable(
                name: "GSTIndia_TaxClass",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 450, nullable: false),
                    HSNCode = table.Column<string>(maxLength: 8, nullable: true),
                    SAC = table.Column<string>(maxLength: 6, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GSTIndia_TaxClass", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders_OrderItemTax",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TaxType = table.Column<string>(nullable: true),
                    Rate = table.Column<decimal>(nullable: false),
                    TaxAmount = table.Column<decimal>(nullable: false),
                    OrderItemId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders_OrderItemTax", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_OrderItemTax_Orders_OrderItem_OrderItemId",
                        column: x => x.OrderItemId,
                        principalTable: "Orders_OrderItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GSTIndia_TaxRate",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TaxClassId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    CountryId = table.Column<string>(maxLength: 450, nullable: true),
                    StateOrProvinceId = table.Column<long>(nullable: true),
                    Rate = table.Column<decimal>(nullable: false),
                    ZipCode = table.Column<string>(maxLength: 450, nullable: true),
                    MinPriceRange = table.Column<decimal>(nullable: true),
                    MaxPriceRange = table.Column<decimal>(nullable: true),
                    TaxType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GSTIndia_TaxRate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GSTIndia_TaxRate_Core_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Core_Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GSTIndia_TaxRate_Core_StateOrProvince_StateOrProvinceId",
                        column: x => x.StateOrProvinceId,
                        principalTable: "Core_StateOrProvince",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GSTIndia_TaxRate_GSTIndia_TaxClass_TaxClassId",
                        column: x => x.TaxClassId,
                        principalTable: "GSTIndia_TaxClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "GSTIndia_TaxClass",
                columns: new[] { "Id", "HSNCode", "Name", "SAC" },
                values: new object[] { 1L, null, "Standard GST", null });

            migrationBuilder.CreateIndex(
                name: "IX_GSTIndia_TaxRate_CountryId",
                table: "GSTIndia_TaxRate",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_GSTIndia_TaxRate_StateOrProvinceId",
                table: "GSTIndia_TaxRate",
                column: "StateOrProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_GSTIndia_TaxRate_TaxClassId",
                table: "GSTIndia_TaxRate",
                column: "TaxClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderItemTax_OrderItemId",
                table: "Orders_OrderItemTax",
                column: "OrderItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Catalog_Product_GSTIndia_TaxClass_TaxClassId",
                table: "Catalog_Product",
                column: "TaxClassId",
                principalTable: "GSTIndia_TaxClass",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Catalog_Product_GSTIndia_TaxClass_TaxClassId",
                table: "Catalog_Product");

            migrationBuilder.DropTable(
                name: "GSTIndia_TaxRate");

            migrationBuilder.DropTable(
                name: "Orders_OrderItemTax");

            migrationBuilder.DropTable(
                name: "GSTIndia_TaxClass");

            migrationBuilder.CreateTable(
                name: "Tax_TaxClass",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tax_TaxClass", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tax_TaxRate",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CountryId = table.Column<string>(maxLength: 450, nullable: true),
                    MaxPriceRange = table.Column<decimal>(nullable: true),
                    MinPriceRange = table.Column<decimal>(nullable: true),
                    Rate = table.Column<decimal>(nullable: false),
                    StateOrProvinceId = table.Column<long>(nullable: true),
                    TaxClassId = table.Column<long>(nullable: false),
                    ZipCode = table.Column<string>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tax_TaxRate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tax_TaxRate_Core_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Core_Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tax_TaxRate_Core_StateOrProvince_StateOrProvinceId",
                        column: x => x.StateOrProvinceId,
                        principalTable: "Core_StateOrProvince",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tax_TaxRate_Tax_TaxClass_TaxClassId",
                        column: x => x.TaxClassId,
                        principalTable: "Tax_TaxClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Tax_TaxClass",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1L, "Standard VAT" });

            migrationBuilder.CreateIndex(
                name: "IX_Tax_TaxRate_CountryId",
                table: "Tax_TaxRate",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tax_TaxRate_StateOrProvinceId",
                table: "Tax_TaxRate",
                column: "StateOrProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Tax_TaxRate_TaxClassId",
                table: "Tax_TaxRate",
                column: "TaxClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Catalog_Product_Tax_TaxClass_TaxClassId",
                table: "Catalog_Product",
                column: "TaxClassId",
                principalTable: "Tax_TaxClass",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
