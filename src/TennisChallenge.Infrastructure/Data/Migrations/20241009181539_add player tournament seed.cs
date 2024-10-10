using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TennisChallenge.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class addplayertournamentseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PlayerTournaments",
                columns: new[] { "PlayerId", "TournamentId" },
                values: new object[,]
                {
                    { new Guid("02891735-bd59-4c05-b5bd-9e203ec8f45e"), new Guid("c3d4097b-d01e-4e28-aa64-56233b158d83") },
                    { new Guid("4b9d627f-836e-4c43-954c-baaf1053a035"), new Guid("c3d4097b-d01e-4e28-aa64-56233b158d83") },
                    { new Guid("8354cd01-71fa-47c2-a66f-6042ee0907ac"), new Guid("c3d4097b-d01e-4e28-aa64-56233b158d83") },
                    { new Guid("b5e2c59e-d935-45b6-b087-1d74e4e11378"), new Guid("c3d4097b-d01e-4e28-aa64-56233b158d83") }
                });

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: new Guid("1f783aab-2a28-4371-917a-330b56f6bf1a"),
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 10, 7, 18, 15, 39, 48, DateTimeKind.Utc).AddTicks(8250), new DateTime(2024, 10, 1, 18, 15, 39, 48, DateTimeKind.Utc).AddTicks(8249) });

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: new Guid("6102ca09-c32a-4b55-9938-f4d39ec8afad"),
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 9, 29, 18, 15, 39, 48, DateTimeKind.Utc).AddTicks(8226), new DateTime(2024, 9, 24, 18, 15, 39, 48, DateTimeKind.Utc).AddTicks(8220) });

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: new Guid("8d62d922-192d-40cf-acc4-8430b030c37a"),
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 10, 9, 18, 15, 39, 48, DateTimeKind.Utc).AddTicks(8254), new DateTime(2024, 10, 4, 18, 15, 39, 48, DateTimeKind.Utc).AddTicks(8254) });

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: new Guid("c3d4097b-d01e-4e28-aa64-56233b158d83"),
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 10, 9, 18, 15, 39, 48, DateTimeKind.Utc).AddTicks(8296), new DateTime(2024, 10, 4, 18, 15, 39, 48, DateTimeKind.Utc).AddTicks(8295) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlayerTournaments",
                keyColumns: new[] { "PlayerId", "TournamentId" },
                keyValues: new object[] { new Guid("02891735-bd59-4c05-b5bd-9e203ec8f45e"), new Guid("c3d4097b-d01e-4e28-aa64-56233b158d83") });

            migrationBuilder.DeleteData(
                table: "PlayerTournaments",
                keyColumns: new[] { "PlayerId", "TournamentId" },
                keyValues: new object[] { new Guid("4b9d627f-836e-4c43-954c-baaf1053a035"), new Guid("c3d4097b-d01e-4e28-aa64-56233b158d83") });

            migrationBuilder.DeleteData(
                table: "PlayerTournaments",
                keyColumns: new[] { "PlayerId", "TournamentId" },
                keyValues: new object[] { new Guid("8354cd01-71fa-47c2-a66f-6042ee0907ac"), new Guid("c3d4097b-d01e-4e28-aa64-56233b158d83") });

            migrationBuilder.DeleteData(
                table: "PlayerTournaments",
                keyColumns: new[] { "PlayerId", "TournamentId" },
                keyValues: new object[] { new Guid("b5e2c59e-d935-45b6-b087-1d74e4e11378"), new Guid("c3d4097b-d01e-4e28-aa64-56233b158d83") });

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: new Guid("1f783aab-2a28-4371-917a-330b56f6bf1a"),
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 10, 6, 1, 3, 17, 726, DateTimeKind.Utc).AddTicks(6915), new DateTime(2024, 9, 30, 1, 3, 17, 726, DateTimeKind.Utc).AddTicks(6914) });

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: new Guid("6102ca09-c32a-4b55-9938-f4d39ec8afad"),
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 9, 28, 1, 3, 17, 726, DateTimeKind.Utc).AddTicks(6897), new DateTime(2024, 9, 23, 1, 3, 17, 726, DateTimeKind.Utc).AddTicks(6889) });

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: new Guid("8d62d922-192d-40cf-acc4-8430b030c37a"),
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 10, 8, 1, 3, 17, 726, DateTimeKind.Utc).AddTicks(6920), new DateTime(2024, 10, 3, 1, 3, 17, 726, DateTimeKind.Utc).AddTicks(6919) });

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: new Guid("c3d4097b-d01e-4e28-aa64-56233b158d83"),
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 10, 8, 1, 3, 17, 726, DateTimeKind.Utc).AddTicks(6924), new DateTime(2024, 10, 3, 1, 3, 17, 726, DateTimeKind.Utc).AddTicks(6923) });
        }
    }
}
