using Microsoft.EntityFrameworkCore.Migrations;

namespace BudgetPlanner.Migrations
{
    public partial class AddExtraData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bedrijf",
                table: "Ingaves",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductImage",
                table: "Ingaves",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Ingaves",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bedrijf",
                table: "Ingaves");

            migrationBuilder.DropColumn(
                name: "ProductImage",
                table: "Ingaves");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Ingaves");
        }
    }
}
