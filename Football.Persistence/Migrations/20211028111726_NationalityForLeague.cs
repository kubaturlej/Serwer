using Microsoft.EntityFrameworkCore.Migrations;

namespace Football.Persistence.Migrations
{
    public partial class NationalityForLeague : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "Leagues",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "Leagues");
        }
    }
}
