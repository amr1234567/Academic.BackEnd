using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Academic.Repository.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AcceptedAt",
                table: "Paths",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAccepted",
                table: "Paths",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AcceptedAt",
                table: "Modules",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAccepted",
                table: "Modules",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Salt",
                table: "IdentityUsers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.DropColumn(
                name: "AcceptedAt",
                table: "Paths");

            migrationBuilder.DropColumn(
                name: "IsAccepted",
                table: "Paths");

            migrationBuilder.DropColumn(
                name: "AcceptedAt",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "IsAccepted",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "Salt",
                table: "IdentityUsers");


        }
    }
}
