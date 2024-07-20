using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyOrderManagementService.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApprovalStatus = table.Column<bool>(type: "bit", nullable: false),
                    OrderPermissionStartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    OrderPermissionEndTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyId);
                    table.UniqueConstraint("AK_Companies_CompanyName", x => x.CompanyName);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.UniqueConstraint("AK_Products_ProductName", x => x.ProductName);
                    table.ForeignKey(
                        name: "FK_Products_Companies_CompanyName",
                        column: x => x.CompanyName,
                        principalTable: "Companies",
                        principalColumn: "CompanyName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Companies_CompanyName",
                        column: x => x.CompanyName,
                        principalTable: "Companies",
                        principalColumn: "CompanyName",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Products_ProductName",
                        column: x => x.ProductName,
                        principalTable: "Products",
                        principalColumn: "ProductName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CompanyName",
                table: "Orders",
                column: "CompanyName");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductName",
                table: "Orders",
                column: "ProductName");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CompanyName",
                table: "Products",
                column: "CompanyName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
