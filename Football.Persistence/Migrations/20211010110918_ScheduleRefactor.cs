using Microsoft.EntityFrameworkCore.Migrations;

namespace Football.Persistence.Migrations
{
    public partial class ScheduleRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rounds_Schedules_ScheduleId",
                table: "Rounds");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.RenameColumn(
                name: "ScheduleId",
                table: "Rounds",
                newName: "LeagueId");

            migrationBuilder.RenameIndex(
                name: "IX_Rounds_ScheduleId",
                table: "Rounds",
                newName: "IX_Rounds_LeagueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rounds_Leagues_LeagueId",
                table: "Rounds",
                column: "LeagueId",
                principalTable: "Leagues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rounds_Leagues_LeagueId",
                table: "Rounds");

            migrationBuilder.RenameColumn(
                name: "LeagueId",
                table: "Rounds",
                newName: "ScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_Rounds_LeagueId",
                table: "Rounds",
                newName: "IX_Rounds_ScheduleId");

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeagueName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Rounds_Schedules_ScheduleId",
                table: "Rounds",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
