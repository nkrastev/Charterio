using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Charterio.Data.Migrations
{
    public partial class TicketCustomerNameFieldRemoval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "Tickets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
