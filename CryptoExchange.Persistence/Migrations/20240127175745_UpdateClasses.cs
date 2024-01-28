using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoExchange.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateClasses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RequestingCustomerId",
                table: "ExchangeRequests",
                newName: "RequestedCustomerId");

            migrationBuilder.AddColumn<string>(
                name: "ReceivedCustomerId",
                table: "ExchangeRequests",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 1, 27, 17, 57, 45, 733, DateTimeKind.Utc).AddTicks(9759), new DateTime(2024, 1, 27, 17, 57, 45, 733, DateTimeKind.Utc).AddTicks(9794) });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 1, 27, 17, 57, 45, 733, DateTimeKind.Utc).AddTicks(9796), new DateTime(2024, 1, 27, 17, 57, 45, 733, DateTimeKind.Utc).AddTicks(9797) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceivedCustomerId",
                table: "ExchangeRequests");

            migrationBuilder.RenameColumn(
                name: "RequestedCustomerId",
                table: "ExchangeRequests",
                newName: "RequestingCustomerId");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 1, 27, 14, 34, 39, 558, DateTimeKind.Utc).AddTicks(9652), new DateTime(2024, 1, 27, 14, 34, 39, 558, DateTimeKind.Utc).AddTicks(9681) });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 1, 27, 14, 34, 39, 558, DateTimeKind.Utc).AddTicks(9683), new DateTime(2024, 1, 27, 14, 34, 39, 558, DateTimeKind.Utc).AddTicks(9683) });
        }
    }
}
