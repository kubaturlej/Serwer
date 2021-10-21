﻿// <auto-generated />
using System;
using Football.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Football.Persistence.Migrations
{
    [DbContext(typeof(FootballDbContext))]
    [Migration("20211009164802_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Football.Domain.Entities.League", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LeagueName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LeagueProgress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MatchesCompleted")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TotalMatches")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Leagues");
                });

            modelBuilder.Entity("Football.Domain.Entities.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Age")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MyProperty")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nationality")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("Football.Domain.Entities.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AvgGoalsPerMacth")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AvgPossession")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CleanSheets")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Draws")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GoalBalance")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GoalConceded")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GoalScored")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GoalsConcededPerMacth")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GoalsScoredPerMatch")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LeagueId")
                        .HasColumnType("int");

                    b.Property<string>("Losses")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MacthesHistory")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Matches")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Points")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Standing")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeamName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeamNationality")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Wins")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LeagueId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Football.Domain.Entities.Player", b =>
                {
                    b.HasOne("Football.Domain.Entities.Team", "Team")
                        .WithMany("Players")
                        .HasForeignKey("TeamId");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Football.Domain.Entities.Team", b =>
                {
                    b.HasOne("Football.Domain.Entities.League", "League")
                        .WithMany("Teams")
                        .HasForeignKey("LeagueId");

                    b.Navigation("League");
                });

            modelBuilder.Entity("Football.Domain.Entities.League", b =>
                {
                    b.Navigation("Teams");
                });

            modelBuilder.Entity("Football.Domain.Entities.Team", b =>
                {
                    b.Navigation("Players");
                });
#pragma warning restore 612, 618
        }
    }
}