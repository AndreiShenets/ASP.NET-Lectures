using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EntityFrameworkCoreApp.DataStorage.Migrations
{
    public partial class Added_EmailReference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Question",
                newName: "EmailAddress");

            migrationBuilder.AddColumn<Guid>(
                name: "EmailId",
                table: "Question",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Question_EmailId",
                table: "Question",
                column: "EmailId",
                unique: true,
                filter: "[EmailId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Email_EmailId",
                table: "Question",
                column: "EmailId",
                principalTable: "Email",
                principalColumn: "EmailId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_Email_EmailId",
                table: "Question");

            migrationBuilder.DropIndex(
                name: "IX_Question_EmailId",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "EmailId",
                table: "Question");

            migrationBuilder.RenameColumn(
                name: "EmailAddress",
                table: "Question",
                newName: "Email");
        }
    }
}
