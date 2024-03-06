using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseMigrationHandler.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserToTimeRegistration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "AUser");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "TimeRegistrations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AUser",
                type: "character varying(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AUser",
                table: "AUser",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TimeRegistrations_UserId",
                table: "TimeRegistrations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeRegistrations_AUser_UserId",
                table: "TimeRegistrations",
                column: "UserId",
                principalTable: "AUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeRegistrations_AUser_UserId",
                table: "TimeRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_TimeRegistrations_UserId",
                table: "TimeRegistrations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AUser",
                table: "AUser");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TimeRegistrations");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AUser");

            migrationBuilder.RenameTable(
                name: "AUser",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");
        }
    }
}
