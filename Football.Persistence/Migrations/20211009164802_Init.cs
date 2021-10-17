using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Football.Persistence.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Leagues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeagueName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalMatches = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatchesCompleted = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeagueProgress = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leagues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeamNationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GoalsScoredPerMatch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GoalsConcededPerMacth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvgPossession = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Standing = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Matches = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Points = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Wins = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Draws = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Losses = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GoalBalance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GoalScored = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GoalConceded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MacthesHistory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CleanSheets = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvgGoalsPerMacth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeagueId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MyProperty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Players_TeamId",
                table: "Players",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_LeagueId",
                table: "Teams",
                column: "LeagueId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Leagues");
        }
    }
}
