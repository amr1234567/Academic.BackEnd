using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Academic.Repository.Migrations
{
    /// <inheritdoc />
    public partial class intstructorUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Salt",
                table: "IdentityUsers",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ConfirmationToken",
                table: "IdentityUsers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "PasswordIsSet",
                table: "IdentityUsers",
                type: "tinyint(1)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmationToken",
                table: "IdentityUsers");

            migrationBuilder.DropColumn(
                name: "PasswordIsSet",
                table: "IdentityUsers");

            migrationBuilder.UpdateData(
                table: "IdentityUsers",
                keyColumn: "Salt",
                keyValue: null,
                column: "Salt",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Salt",
                table: "IdentityUsers",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
