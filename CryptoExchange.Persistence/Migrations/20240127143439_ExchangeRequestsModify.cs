using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoExchange.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ExchangeRequestsModify : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeRequests_Currencies_CurrencyId",
                table: "ExchangeRequests");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeRequests_CurrencyId",
                table: "ExchangeRequests");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "ExchangeRequests");

            migrationBuilder.RenameColumn(
                name: "CurrencyTypeId",
                table: "ExchangeRequests",
                newName: "CurrencyToExchangeId");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "ExchangeRequests",
                newName: "CurrencyToExchangeAmount");

            migrationBuilder.AddColumn<double>(
                name: "CurrencyForExchangeAmount",
                table: "ExchangeRequests",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "CurrencyForExchangeId",
                table: "ExchangeRequests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRequests_CurrencyForExchangeId",
                table: "ExchangeRequests",
                column: "CurrencyForExchangeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRequests_CurrencyToExchangeId",
                table: "ExchangeRequests",
                column: "CurrencyToExchangeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeRequests_Currencies_CurrencyForExchangeId",
                table: "ExchangeRequests",
                column: "CurrencyForExchangeId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeRequests_Currencies_CurrencyToExchangeId",
                table: "ExchangeRequests",
                column: "CurrencyToExchangeId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeRequests_Currencies_CurrencyForExchangeId",
                table: "ExchangeRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeRequests_Currencies_CurrencyToExchangeId",
                table: "ExchangeRequests");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeRequests_CurrencyForExchangeId",
                table: "ExchangeRequests");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeRequests_CurrencyToExchangeId",
                table: "ExchangeRequests");

            migrationBuilder.DropColumn(
                name: "CurrencyForExchangeAmount",
                table: "ExchangeRequests");

            migrationBuilder.DropColumn(
                name: "CurrencyForExchangeId",
                table: "ExchangeRequests");

            migrationBuilder.RenameColumn(
                name: "CurrencyToExchangeId",
                table: "ExchangeRequests",
                newName: "CurrencyTypeId");

            migrationBuilder.RenameColumn(
                name: "CurrencyToExchangeAmount",
                table: "ExchangeRequests",
                newName: "Amount");

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "ExchangeRequests",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 1, 27, 1, 21, 18, 462, DateTimeKind.Utc).AddTicks(5535), new DateTime(2024, 1, 27, 1, 21, 18, 462, DateTimeKind.Utc).AddTicks(5573) });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 1, 27, 1, 21, 18, 462, DateTimeKind.Utc).AddTicks(5576), new DateTime(2024, 1, 27, 1, 21, 18, 462, DateTimeKind.Utc).AddTicks(5577) });

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRequests_CurrencyId",
                table: "ExchangeRequests",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeRequests_Currencies_CurrencyId",
                table: "ExchangeRequests",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id");
        }
    }
}
