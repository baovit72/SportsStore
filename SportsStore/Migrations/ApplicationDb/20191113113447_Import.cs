using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsStore.Migrations
{
    public partial class Import : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WholeSalerInfo",
                columns: table => new
                {
                    WholeSalerInfoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Line1 = table.Column<string>(nullable: false),
                    Line2 = table.Column<string>(nullable: true),
                    Line3 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: false),
                    State = table.Column<string>(nullable: false),
                    Zip = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: false),
                    GiftWrap = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WholeSalerInfo", x => x.WholeSalerInfoID);
                });

            migrationBuilder.CreateTable(
                name: "ImportOrders",
                columns: table => new
                {
                    ImportOrderID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Received = table.Column<bool>(nullable: false),
                    Sum = table.Column<decimal>(nullable: false),
                    SalerInfoWholeSalerInfoID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportOrders", x => x.ImportOrderID);
                    table.ForeignKey(
                        name: "FK_ImportOrders_WholeSalerInfo_SalerInfoWholeSalerInfoID",
                        column: x => x.SalerInfoWholeSalerInfoID,
                        principalTable: "WholeSalerInfo",
                        principalColumn: "WholeSalerInfoID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ImportItemsLine",
                columns: table => new
                {
                    ImportItemsLineID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductID = table.Column<int>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    ImportOrderID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportItemsLine", x => x.ImportItemsLineID);
                    table.ForeignKey(
                        name: "FK_ImportItemsLine_ImportOrders_ImportOrderID",
                        column: x => x.ImportOrderID,
                        principalTable: "ImportOrders",
                        principalColumn: "ImportOrderID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImportItemsLine_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImportItemsLine_ImportOrderID",
                table: "ImportItemsLine",
                column: "ImportOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_ImportItemsLine_ProductID",
                table: "ImportItemsLine",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ImportOrders_SalerInfoWholeSalerInfoID",
                table: "ImportOrders",
                column: "SalerInfoWholeSalerInfoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImportItemsLine");

            migrationBuilder.DropTable(
                name: "ImportOrders");

            migrationBuilder.DropTable(
                name: "WholeSalerInfo");
        }
    }
}
