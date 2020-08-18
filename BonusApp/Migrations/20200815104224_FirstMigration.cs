using Microsoft.EntityFrameworkCore.Migrations;

namespace BonusApp.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Coupon",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: false),
                    Pages = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupon", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientCoupon",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClientId = table.Column<int>(nullable: false),
                    CouponId = table.Column<int>(nullable: false),
                    SpentPages = table.Column<int>(nullable: false),
                    Date = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientCoupon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientCoupon_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientCoupon_Coupon_CouponId",
                        column: x => x.CouponId,
                        principalTable: "Coupon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClientId = table.Column<int>(nullable: false),
                    CouponId = table.Column<int>(nullable: false),
                    ClientCouponId = table.Column<int>(nullable: false),
                    SpentPages = table.Column<int>(nullable: false),
                    Date = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_ClientCoupon_ClientCouponId",
                        column: x => x.ClientCouponId,
                        principalTable: "ClientCoupon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transaction_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transaction_Coupon_CouponId",
                        column: x => x.CouponId,
                        principalTable: "Coupon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Client",
                columns: new[] { "Id", "Email" },
                values: new object[] { 1, "TRCtester@tcr.com" });

            migrationBuilder.InsertData(
                table: "Coupon",
                columns: new[] { "Id", "Name", "Pages" },
                values: new object[] { 1, "BYN100", 100 });

            migrationBuilder.InsertData(
                table: "Coupon",
                columns: new[] { "Id", "Name", "Pages" },
                values: new object[] { 2, "BYN200", 200 });

            migrationBuilder.InsertData(
                table: "Coupon",
                columns: new[] { "Id", "Name", "Pages" },
                values: new object[] { 3, "BYN500", 500 });

            migrationBuilder.InsertData(
                table: "Coupon",
                columns: new[] { "Id", "Name", "Pages" },
                values: new object[] { 4, "COLOR50", 50 });

            migrationBuilder.InsertData(
                table: "Coupon",
                columns: new[] { "Id", "Name", "Pages" },
                values: new object[] { 5, "COLOR100", 100 });

            migrationBuilder.InsertData(
                table: "Coupon",
                columns: new[] { "Id", "Name", "Pages" },
                values: new object[] { 6, "COLOR200", 200 });

            migrationBuilder.CreateIndex(
                name: "IX_ClientCoupon_ClientId",
                table: "ClientCoupon",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientCoupon_CouponId",
                table: "ClientCoupon",
                column: "CouponId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_ClientCouponId",
                table: "Transaction",
                column: "ClientCouponId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_ClientId",
                table: "Transaction",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CouponId",
                table: "Transaction",
                column: "CouponId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "ClientCoupon");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Coupon");
        }
    }
}