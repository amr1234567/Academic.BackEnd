using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Academic.Repository.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "IdentityUsers",
                type: "varchar(100)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiredAt",
                table: "IdentityUsers",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "IdentityUsers",
                type: "ENUM('Admin','Instructor','Student')",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "IdentityUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiredAt",
                table: "IdentityUsers");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "IdentityUsers");
        }
    }
}
