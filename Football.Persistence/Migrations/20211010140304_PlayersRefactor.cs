using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Football.Persistence.Migrations
{
    public partial class PlayersRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "MyProperty",
                table: "Players",
                newName: "YellowCards");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Players",
                newName: "StartedFromBegin");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Players",
                newName: "RedCards");

            migrationBuilder.AddColumn<string>(
                name: "Assists",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Goals",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MatchesPlayed",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MinutesPlayed",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Assists",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Goals",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "MatchesPlayed",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "MinutesPlayed",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "YellowCards",
                table: "Players",
                newName: "MyProperty");

            migrationBuilder.RenameColumn(
                name: "StartedFromBegin",
                table: "Players",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "RedCards",
                table: "Players",
                newName: "FirstName");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Players",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
