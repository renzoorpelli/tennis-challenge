using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TennisChallenge.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Country = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Level = table.Column<long>(type: "bigint", nullable: false),
                    Age = table.Column<long>(type: "bigint", nullable: false),
                    Gender = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    Wins = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    Losses = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    ReactionTime = table.Column<long>(type: "bigint", nullable: true, defaultValue: 20L),
                    Force = table.Column<long>(type: "bigint", nullable: true, defaultValue: 20L),
                    Velocity = table.Column<long>(type: "bigint", nullable: true, defaultValue: 20L)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tournaments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    WinnerId = table.Column<Guid>(type: "uuid", nullable: true),
                    TournamentType = table.Column<string>(type: "text", nullable: false),
                    MatchesCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournaments", x => x.Id);
                    table.CheckConstraint("CK_Tournaments_TournamentType", "\"TournamentType\" IN ('Male', 'Female')");
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PlayerOneId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlayerTwoId = table.Column<Guid>(type: "uuid", nullable: false),
                    TournamentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Round = table.Column<int>(type: "integer", nullable: false),
                    PlayerOnePoints = table.Column<int>(type: "integer", nullable: false),
                    PlayerTwoPoints = table.Column<int>(type: "integer", nullable: false),
                    WinnerId = table.Column<Guid>(type: "uuid", nullable: true),
                    MatchDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matches_Players_PlayerOneId",
                        column: x => x.PlayerOneId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Players_PlayerTwoId",
                        column: x => x.PlayerTwoId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerTournament",
                columns: table => new
                {
                    PlayersId = table.Column<Guid>(type: "uuid", nullable: false),
                    TournamentsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerTournament", x => new { x.PlayersId, x.TournamentsId });
                    table.ForeignKey(
                        name: "FK_PlayerTournament_Players_PlayersId",
                        column: x => x.PlayersId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerTournament_Tournaments_TournamentsId",
                        column: x => x.TournamentsId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerTournaments",
                columns: table => new
                {
                    PlayerId = table.Column<Guid>(type: "uuid", nullable: false),
                    TournamentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerTournaments", x => new { x.PlayerId, x.TournamentId });
                    table.ForeignKey(
                        name: "FK_PlayerTournaments_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerTournaments_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "Age", "Country", "Force", "Gender", "Level", "Losses", "Name", "Velocity", "Wins" },
                values: new object[,]
                {
                    { new Guid("01416867-5fa7-406b-8509-5e00c0d95291"), 24L, "Ireland", 83L, "Male", 5L, 20L, "Liam O'Connor", 79L, 27L },
                    { new Guid("02891735-bd59-4c05-b5bd-9e203ec8f45e"), 30L, "South Korea", 92L, "Male", 8L, 10L, "Alex Kim", 87L, 60L },
                    { new Guid("4b9d627f-836e-4c43-954c-baaf1053a035"), 28L, "Australia", 95L, "Male", 7L, 8L, "Bob Brown", 90L, 50L }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "Age", "Country", "Gender", "Level", "Losses", "Name", "ReactionTime", "Wins" },
                values: new object[] { new Guid("71b73c9f-1912-434d-9e8f-e22f8ba6a4e9"), 27L, "France", "Female", 7L, 12L, "Sophie Laurent", 85L, 45L });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "Age", "Country", "Force", "Gender", "Level", "Losses", "Name", "Velocity", "Wins" },
                values: new object[] { new Guid("8354cd01-71fa-47c2-a66f-6042ee0907ac"), 26L, "Germany", 88L, "Male", 6L, 12L, "Michael Green", 80L, 35L });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "Age", "Country", "Gender", "Level", "Losses", "Name", "ReactionTime", "Wins" },
                values: new object[] { new Guid("8d8e4428-ac41-47ab-a50f-11001b45ccfa"), 23L, "Spain", "Female", 5L, 18L, "Maria Rodriguez", 82L, 28L });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "Age", "Country", "Force", "Gender", "Level", "Losses", "Name", "Velocity", "Wins" },
                values: new object[] { new Guid("b5e2c59e-d935-45b6-b087-1d74e4e11378"), 25L, "USA", 90L, "Male", 5L, 10L, "John Doe", 85L, 30L });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "Age", "Country", "Gender", "Level", "Losses", "Name", "ReactionTime", "Wins" },
                values: new object[,]
                {
                    { new Guid("c03723d9-369a-4a0b-bbab-f9215222a6d3"), 22L, "Canada", "Female", 4L, 15L, "Alice Johnson", 75L, 25L },
                    { new Guid("d500a7ce-3b77-43d3-81f6-61d31bde1447"), 25L, "Italy", "Female", 6L, 14L, "Isabella Rossi", 78L, 30L },
                    { new Guid("def78e39-829c-4463-ba33-ac51c85d20fc"), 24L, "UK", "Female", 6L, 5L, "Jane Smith", 80L, 40L }
                });

            migrationBuilder.InsertData(
                table: "Tournaments",
                columns: new[] { "Id", "EndDate", "MatchesCount", "Name", "StartDate", "TournamentType", "WinnerId" },
                values: new object[,]
                {
                    { new Guid("1f783aab-2a28-4371-917a-330b56f6bf1a"), new DateTime(2024, 10, 6, 1, 3, 17, 726, DateTimeKind.Utc).AddTicks(6915), 8, "Summer Showdown", new DateTime(2024, 9, 30, 1, 3, 17, 726, DateTimeKind.Utc).AddTicks(6914), "Male", null },
                    { new Guid("6102ca09-c32a-4b55-9938-f4d39ec8afad"), new DateTime(2024, 9, 28, 1, 3, 17, 726, DateTimeKind.Utc).AddTicks(6897), 8, "Spring Championship", new DateTime(2024, 9, 23, 1, 3, 17, 726, DateTimeKind.Utc).AddTicks(6889), "Male", null },
                    { new Guid("8d62d922-192d-40cf-acc4-8430b030c37a"), new DateTime(2024, 10, 8, 1, 3, 17, 726, DateTimeKind.Utc).AddTicks(6920), 8, "Female Autumn Invitational", new DateTime(2024, 10, 3, 1, 3, 17, 726, DateTimeKind.Utc).AddTicks(6919), "Female", null },
                    { new Guid("c3d4097b-d01e-4e28-aa64-56233b158d83"), new DateTime(2024, 10, 8, 1, 3, 17, 726, DateTimeKind.Utc).AddTicks(6924), 8, "Male Autumn Invitational", new DateTime(2024, 10, 3, 1, 3, 17, 726, DateTimeKind.Utc).AddTicks(6923), "Male", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Id",
                table: "Matches",
                column: "Id")
                .Annotation("Npgsql:IndexInclude", new[] { "PlayerOneId", "PlayerTwoId", "TournamentId" });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_PlayerOneId",
                table: "Matches",
                column: "PlayerOneId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_PlayerTwoId",
                table: "Matches",
                column: "PlayerTwoId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TournamentId",
                table: "Matches",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerTournament_TournamentsId",
                table: "PlayerTournament",
                column: "TournamentsId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerTournaments_TournamentId",
                table: "PlayerTournaments",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_Id",
                table: "Tournaments",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_Name",
                table: "Tournaments",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "PlayerTournament");

            migrationBuilder.DropTable(
                name: "PlayerTournaments");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Tournaments");
        }
    }
}
