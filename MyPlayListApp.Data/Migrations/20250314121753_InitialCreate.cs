using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyPlayListApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Singers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Singers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Songs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SingerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Songs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Songs_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Songs_Singers_SingerId",
                        column: x => x.SingerId,
                        principalTable: "Singers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2bacf241-d7dd-4c47-b2ec-a0fb3624bd16"), "Romance" },
                    { new Guid("6e8aac6d-b912-470e-8b94-d24c5864b010"), "Tragedy" },
                    { new Guid("fd43a5ee-bd48-4858-a817-4bd3c05a9f62"), "Rock" }
                });

            migrationBuilder.InsertData(
                table: "Singers",
                columns: new[] { "Id", "DateOfBirth", "Image", "Name" },
                values: new object[,]
                {
                    { new Guid("785ede46-ebc5-4e0d-9722-88f052677543"), new DateOnly(1977, 8, 11), "D:\\PlayListApp\\Images\\TamerHosny.jpg", "Tamer Hosny" },
                    { new Guid("7970bcbe-64ac-465c-94f5-42f4fd6d0d43"), new DateOnly(1980, 10, 10), "D:\\PlayListApp\\Images\\SherineAbdelWahab.jpg", "Sherine Abdel Wahab" },
                    { new Guid("a61a7b3a-ffb8-41d5-9b90-ecc2f57ade36"), new DateOnly(1975, 11, 4), "D:\\PlayListApp\\Images\\MohamedHamaki.jpg", "Mohamed Hamaki" }
                });

            migrationBuilder.InsertData(
                table: "Songs",
                columns: new[] { "Id", "CategoryId", "Name", "SingerId" },
                values: new object[,]
                {
                    { new Guid("4adb51d5-5657-49a1-883f-8e346ef10ee8"), new Guid("6e8aac6d-b912-470e-8b94-d24c5864b010"), "Naseeny Leeh", new Guid("785ede46-ebc5-4e0d-9722-88f052677543") },
                    { new Guid("6e201f3c-f7a7-4c46-9360-bb2e485a8ff5"), new Guid("2bacf241-d7dd-4c47-b2ec-a0fb3624bd16"), "Mashaer", new Guid("7970bcbe-64ac-465c-94f5-42f4fd6d0d43") },
                    { new Guid("d8fe7301-11e4-45a2-a0d6-add109cbff96"), new Guid("fd43a5ee-bd48-4858-a817-4bd3c05a9f62"), "Bahebak Kol Youm", new Guid("a61a7b3a-ffb8-41d5-9b90-ecc2f57ade36") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Songs_CategoryId",
                table: "Songs",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_SingerId",
                table: "Songs",
                column: "SingerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Songs");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Singers");
        }
    }
}
