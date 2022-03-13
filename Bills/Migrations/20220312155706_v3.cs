using Microsoft.EntityFrameworkCore.Migrations;

namespace Bills.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "BillItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalAmount",
                table: "BillItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
