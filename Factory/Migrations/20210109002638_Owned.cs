using Microsoft.EntityFrameworkCore.Migrations;

namespace Factory.Migrations
{
    public partial class Owned : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Owned",
                table: "Machines",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Owned",
                table: "Machines");
        }
    }
}
