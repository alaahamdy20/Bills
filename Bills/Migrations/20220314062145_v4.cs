using Microsoft.EntityFrameworkCore.Migrations;

namespace Bills.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillDetails");

            migrationBuilder.AddColumn<int>(
                name: "BillsTotal",
                table: "Bills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PaidUp",
                table: "Bills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PercentageDiscount",
                table: "Bills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TheNet",
                table: "Bills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TheRest",
                table: "Bills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ValueDiscount",
                table: "Bills",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillsTotal",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "PaidUp",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "PercentageDiscount",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "TheNet",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "TheRest",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "ValueDiscount",
                table: "Bills");

            migrationBuilder.CreateTable(
                name: "BillDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillId = table.Column<int>(type: "int", nullable: false),
                    BillsTotal = table.Column<int>(type: "int", nullable: false),
                    PaidUp = table.Column<int>(type: "int", nullable: false),
                    PercentageDiscount = table.Column<int>(type: "int", nullable: false),
                    TheNet = table.Column<int>(type: "int", nullable: false),
                    TheRest = table.Column<int>(type: "int", nullable: false),
                    ValueDiscount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillDetails_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_BillId",
                table: "BillDetails",
                column: "BillId",
                unique: true);
        }
    }
}
