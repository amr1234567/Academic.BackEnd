using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Academic.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddNewEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserQuestionAnswer",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int(8)", nullable: false),
                    QuizId = table.Column<int>(type: "int(8)", nullable: false),
                    QuestionId = table.Column<int>(type: "int(8)", nullable: false),
                    UserChoice = table.Column<string>(type: "varchar(1)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsCorrect = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserQuestionAnswer", x => new { x.QuizId, x.QuestionId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserQuestionAnswer_IdentityUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "IdentityUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserQuestionAnswer_MultiChoiceQuestions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "MultiChoiceQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserQuestionAnswer_Quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_UserQuestionAnswer_QuestionId",
                table: "UserQuestionAnswer",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserQuestionAnswer_UserId",
                table: "UserQuestionAnswer",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserQuestionAnswer");
        }
    }
}
