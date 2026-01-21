using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HostMarket.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "transactionStatus",
                schema: "hostmarket",
                table: "Transactions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ContainerId",
                schema: "hostmarket",
                table: "Servers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IP",
                schema: "hostmarket",
                table: "Servers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Port",
                schema: "hostmarket",
                table: "Servers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "RentalEnd",
                schema: "hostmarket",
                table: "Servers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RentalStart",
                schema: "hostmarket",
                table: "Servers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TariffId",
                schema: "hostmarket",
                table: "Servers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Tariffs",
                schema: "hostmarket",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TariffName = table.Column<string>(type: "text", nullable: false),
                    RamGb = table.Column<decimal>(type: "numeric", nullable: false),
                    DiskGb = table.Column<decimal>(type: "numeric", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tariffs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Servers_TariffId",
                schema: "hostmarket",
                table: "Servers",
                column: "TariffId");

            migrationBuilder.AddForeignKey(
                name: "FK_Servers_Tariffs_TariffId",
                schema: "hostmarket",
                table: "Servers",
                column: "TariffId",
                principalSchema: "hostmarket",
                principalTable: "Tariffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servers_Tariffs_TariffId",
                schema: "hostmarket",
                table: "Servers");

            migrationBuilder.DropTable(
                name: "Tariffs",
                schema: "hostmarket");

            migrationBuilder.DropIndex(
                name: "IX_Servers_TariffId",
                schema: "hostmarket",
                table: "Servers");

            migrationBuilder.DropColumn(
                name: "transactionStatus",
                schema: "hostmarket",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ContainerId",
                schema: "hostmarket",
                table: "Servers");

            migrationBuilder.DropColumn(
                name: "IP",
                schema: "hostmarket",
                table: "Servers");

            migrationBuilder.DropColumn(
                name: "Port",
                schema: "hostmarket",
                table: "Servers");

            migrationBuilder.DropColumn(
                name: "RentalEnd",
                schema: "hostmarket",
                table: "Servers");

            migrationBuilder.DropColumn(
                name: "RentalStart",
                schema: "hostmarket",
                table: "Servers");

            migrationBuilder.DropColumn(
                name: "TariffId",
                schema: "hostmarket",
                table: "Servers");
        }
    }
}
