using Microsoft.EntityFrameworkCore.Migrations;

namespace MyBank.Data.Migrations
{
    public partial class AddFieldForBalance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Amount",
                table: "Balances",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Balances");
        }
    }
}
