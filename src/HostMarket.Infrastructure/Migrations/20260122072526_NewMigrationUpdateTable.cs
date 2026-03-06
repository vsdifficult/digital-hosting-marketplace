using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HostMarket.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewMigrationUpdateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servers_Users_UserId",
                schema: "hostmarket",
                table: "Servers");

            migrationBuilder.AlterColumn<Guid>(
                name: "ownerId",
                schema: "hostmarket",
                table: "Servers",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                schema: "hostmarket",
                table: "Servers",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Servers_Users_UserId",
                schema: "hostmarket",
                table: "Servers",
                column: "UserId",
                principalSchema: "hostmarket",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servers_Users_UserId",
                schema: "hostmarket",
                table: "Servers");

            migrationBuilder.AlterColumn<Guid>(
                name: "ownerId",
                schema: "hostmarket",
                table: "Servers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                schema: "hostmarket",
                table: "Servers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Servers_Users_UserId",
                schema: "hostmarket",
                table: "Servers",
                column: "UserId",
                principalSchema: "hostmarket",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
