using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EntityFrameworkCoreApp.DataStorage.Migrations
{
    public partial class Added_NewFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDateTimeUTC",
                table: "Question",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Question",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsClosed",
                table: "Question",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "Question",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Question",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDateTimeUTC",
                table: "Answer",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDateTimeUTC",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "IsClosed",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "CreateDateTimeUTC",
                table: "Answer");
        }
    }
}
