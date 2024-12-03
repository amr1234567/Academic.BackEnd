using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Academic.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "IdentityUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int(8)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(type: "varchar(255)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Phone = table.Column<string>(type: "varchar(20)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Discriminator = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HashPassword = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HashedPassword = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Title = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    JobType = table.Column<string>(type: "varchar(255)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    Country = table.Column<string>(type: "varchar(255)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsLocked = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    Points = table.Column<float>(type: "float(4,2)", nullable: true, defaultValue: 0f),
                    Education_EducationalClass = table.Column<string>(type: "ENUM('First','Second','Third','Fourth','Fifth','Sixth','Seventh')", nullable: false),
                    Education_EducationalLevel = table.Column<string>(type: "ENUM('Secondary','Preparatory','Graduated','Undergraduate')", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUsers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Quizzes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int(8)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "varchar(255)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SectionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizzes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MultiChoiceQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int(8)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ChoiceA = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ChoiceB = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ChoiceC = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ChoiceD = table.Column<string>(type: "varchar(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Answer = table.Column<string>(type: "char(2)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Points = table.Column<float>(type: "float(4,2)", nullable: false, defaultValue: 1f),
                    InstructorId = table.Column<int>(type: "int(8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultiChoiceQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MultiChoiceQuestions_IdentityUsers_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "IdentityUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QuizQuestions",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "int(8)", nullable: false),
                    QuizId = table.Column<int>(type: "int(8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizQuestions", x => new { x.QuestionId, x.QuizId });
                   
                    table.ForeignKey(
                        name: "FK_QuizQuestions_MultiChoiceQuestions_QuizId",
                        column: x => x.QuizId,
                        principalTable: "MultiChoiceQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuizQuestions_Quizzes_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                   
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "userQuestionAnswers",
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
                    table.PrimaryKey("PK_userQuestionAnswers", x => new { x.QuizId, x.QuestionId, x.UserId });
                    table.ForeignKey(
                        name: "FK_userQuestionAnswers_IdentityUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "IdentityUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userQuestionAnswers_MultiChoiceQuestions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "MultiChoiceQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userQuestionAnswers_Quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int(8)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "varchar(255)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Difficulty = table.Column<float>(type: "float(1,1)", nullable: false),
                    Description = table.Column<string>(type: "varchar(255)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NumOfSections = table.Column<int>(type: "int(4)", nullable: false, defaultValue: 1),
                    ExpectedTimeToComplete = table.Column<TimeSpan>(type: "Time", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    PathId = table.Column<int>(type: "int(8)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ModuleSections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int(8)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "varchar(255)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Body = table.Column<string>(type: "LONGTEXT", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    QuizId = table.Column<int>(type: "int(8)", nullable: false),
                    ModuleId = table.Column<int>(type: "int(8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModuleSections_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ModuleSections_Quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ModuleUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int(8)", nullable: false),
                    ModuleId = table.Column<int>(type: "int(8)", nullable: false),
                    ProgressPresented = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleUsers", x => new { x.UserId, x.ModuleId });
                    table.ForeignKey(
                        name: "FK_ModuleUsers_IdentityUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "IdentityUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ModuleUsers_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ModuleSectionUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int(8)", nullable: false),
                    ModuleSectionId = table.Column<int>(type: "int(8)", nullable: false),
                    ProgressPresented = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleSectionUsers", x => new { x.UserId, x.ModuleSectionId });
                    table.ForeignKey(
                        name: "FK_ModuleSectionUsers_IdentityUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "IdentityUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ModuleSectionUsers_ModuleSections_ModuleSectionId",
                        column: x => x.ModuleSectionId,
                        principalTable: "ModuleSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Paths",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int(8)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "varchar(255)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "MEDIUMTEXT", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IntroductionBody = table.Column<string>(type: "LONGTEXT", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Difficulty = table.Column<float>(type: "float(2,2)", nullable: false),
                    NumOfModules = table.Column<int>(type: "int(4)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    InstructorId = table.Column<int>(type: "int(8)", nullable: false),
                    PathTaskId = table.Column<int>(type: "int(8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paths", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Paths_IdentityUsers_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "IdentityUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PathTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int(8)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TotalPoints = table.Column<float>(type: "float(4,2)", nullable: false, defaultValue: 1f),
                    MinPointsToCertify = table.Column<float>(type: "float(4,2)", nullable: false, defaultValue: 1f),
                    Description = table.Column<string>(type: "MEDIUMTEXT", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PathId = table.Column<int>(type: "int(8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PathTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PathTasks_Paths_PathId",
                        column: x => x.PathId,
                        principalTable: "Paths",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PathUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int(8)", nullable: false),
                    PathId = table.Column<int>(type: "int(8)", nullable: false),
                    NumberOfCompletedModules = table.Column<int>(type: "int", nullable: false),
                    IsCompleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PathUsers", x => new { x.UserId, x.PathId });
                    table.ForeignKey(
                        name: "FK_PathUsers_IdentityUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "IdentityUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PathUsers_Paths_PathId",
                        column: x => x.PathId,
                        principalTable: "Paths",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PathTaskQuestions",
                columns: table => new
                {
                    PathTaskId = table.Column<int>(type: "int(8)", nullable: false),
                    QuestionId = table.Column<int>(type: "int(8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PathTaskQuestions", x => new { x.PathTaskId, x.QuestionId });
                    table.ForeignKey(
                        name: "FK_PathTaskQuestions_MultiChoiceQuestions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "MultiChoiceQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PathTaskQuestions_PathTasks_PathTaskId",
                        column: x => x.PathTaskId,
                        principalTable: "PathTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PathTaskUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int(8)", nullable: false),
                    PathTaskId = table.Column<int>(type: "int(8)", nullable: false),
                    HasCertification = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Score = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PathTaskUsers", x => new { x.UserId, x.PathTaskId });
                    table.ForeignKey(
                        name: "FK_PathTaskUsers_IdentityUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "IdentityUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PathTaskUsers_PathTasks_PathTaskId",
                        column: x => x.PathTaskId,
                        principalTable: "PathTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_PathId",
                table: "Modules",
                column: "PathId");

           

            migrationBuilder.CreateIndex(
                name: "IX_ModuleSections_ModuleId",
                table: "ModuleSections",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleSections_QuizId",
                table: "ModuleSections",
                column: "QuizId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModuleSectionUsers_ModuleSectionId",
                table: "ModuleSectionUsers",
                column: "ModuleSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleUsers_ModuleId",
                table: "ModuleUsers",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_MultiChoiceQuestions_InstructorId",
                table: "MultiChoiceQuestions",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Paths_InstructorId",
                table: "Paths",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Paths_PathTaskId",
                table: "Paths",
                column: "PathTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_PathTaskQuestions_QuestionId",
                table: "PathTaskQuestions",
                column: "QuestionId");

           

            migrationBuilder.CreateIndex(
                name: "IX_PathTasks_PathId",
                table: "PathTasks",
                column: "PathId");

            migrationBuilder.CreateIndex(
                name: "IX_PathTaskUsers_PathTaskId",
                table: "PathTaskUsers",
                column: "PathTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_PathUsers_PathId",
                table: "PathUsers",
                column: "PathId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuestions_QuestionId",
                table: "QuizQuestions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuestions_QuizId",
                table: "QuizQuestions",
                column: "QuizId");

            

            migrationBuilder.CreateIndex(
                name: "IX_userQuestionAnswers_QuestionId",
                table: "userQuestionAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_userQuestionAnswers_UserId",
                table: "userQuestionAnswers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Paths_PathId",
                table: "Modules",
                column: "PathId",
                principalTable: "Paths",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

           

            migrationBuilder.AddForeignKey(
                name: "FK_Paths_PathTasks_PathTaskId",
                table: "Paths",
                column: "PathTaskId",
                principalTable: "PathTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PathTasks_Paths_PathId",
                table: "PathTasks");

            migrationBuilder.DropTable(
                name: "ModuleSectionUsers");

            migrationBuilder.DropTable(
                name: "ModuleUsers");

            migrationBuilder.DropTable(
                name: "PathTaskQuestions");

            migrationBuilder.DropTable(
                name: "PathTaskUsers");

            migrationBuilder.DropTable(
                name: "PathUsers");

            migrationBuilder.DropTable(
                name: "QuizQuestions");

            migrationBuilder.DropTable(
                name: "userQuestionAnswers");

            migrationBuilder.DropTable(
                name: "ModuleSections");

            migrationBuilder.DropTable(
                name: "MultiChoiceQuestions");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "Quizzes");

            migrationBuilder.DropTable(
                name: "Paths");

            migrationBuilder.DropTable(
                name: "IdentityUsers");

            migrationBuilder.DropTable(
                name: "PathTasks");
        }
    }
}
