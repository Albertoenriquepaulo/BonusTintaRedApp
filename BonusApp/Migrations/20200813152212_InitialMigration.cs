using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BonusApp.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bonus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: false),
                    Pages = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bonus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BonusSpending",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(nullable: false),
                    BonusId = table.Column<int>(nullable: false),
                    SpentPages = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonusSpending", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BonusSpending_Bonus_BonusId",
                        column: x => x.BonusId,
                        principalTable: "Bonus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BonusSpending_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserBonus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(nullable: false),
                    BonusId = table.Column<int>(nullable: false),
                    SpentPages = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBonus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBonus_Bonus_BonusId",
                        column: x => x.BonusId,
                        principalTable: "Bonus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserBonus_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email" },
                values: new object[] { 1, "email@email.com" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email" },
                values: new object[] { 2, "Alan@email.com" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email" },
                values: new object[] { 3, "Tom@email.com" });

            migrationBuilder.CreateIndex(
                name: "IX_BonusSpending_BonusId",
                table: "BonusSpending",
                column: "BonusId");

            migrationBuilder.CreateIndex(
                name: "IX_BonusSpending_UserId",
                table: "BonusSpending",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBonus_BonusId",
                table: "UserBonus",
                column: "BonusId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBonus_UserId",
                table: "UserBonus",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BonusSpending");

            migrationBuilder.DropTable(
                name: "UserBonus");

            migrationBuilder.DropTable(
                name: "Bonus");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
