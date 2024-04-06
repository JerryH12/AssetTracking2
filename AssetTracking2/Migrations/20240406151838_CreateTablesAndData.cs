using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AssetTracking2.Migrations
{
    /// <inheritdoc />
    public partial class CreateTablesAndData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Office = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PriceInUSD = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LaptopComputers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssetsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaptopComputers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LaptopComputers_Assets_AssetsId",
                        column: x => x.AssetsId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MobilePhones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssetsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobilePhones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MobilePhones_Assets_AssetsId",
                        column: x => x.AssetsId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Assets",
                columns: new[] { "Id", "Brand", "Office", "PriceInUSD", "PurchaseDate" },
                values: new object[,]
                {
                    { 1, "Macbook", "USA", 1200.0, new DateTime(2021, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Samsung", "USA", 200.0, new DateTime(2021, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Dell", "India", 599.0, new DateTime(2014, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Motorola", "India", 110.0, new DateTime(2021, 9, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Lenovo", "Sweden", 599.0, new DateTime(2021, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "Asus", "Sweden", 490.0, new DateTime(2023, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "Nokia", "USA", 230.0, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "LaptopComputers",
                columns: new[] { "Id", "AssetsId", "Model", "Type" },
                values: new object[,]
                {
                    { 1, 1, "Air", "Computer" },
                    { 3, 3, "ThinkPad", "Computer" },
                    { 4, 5, "IdeaPad", "Computer" },
                    { 5, 6, "Vivobook", "Computer" }
                });

            migrationBuilder.InsertData(
                table: "MobilePhones",
                columns: new[] { "Id", "AssetsId", "Model", "Type" },
                values: new object[,]
                {
                    { 1, 2, "SD", "Mobile" },
                    { 2, 4, "G04", "Mobile" },
                    { 3, 7, "G22", "Mobile" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LaptopComputers_AssetsId",
                table: "LaptopComputers",
                column: "AssetsId");

            migrationBuilder.CreateIndex(
                name: "IX_MobilePhones_AssetsId",
                table: "MobilePhones",
                column: "AssetsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LaptopComputers");

            migrationBuilder.DropTable(
                name: "MobilePhones");

            migrationBuilder.DropTable(
                name: "Assets");
        }
    }
}
