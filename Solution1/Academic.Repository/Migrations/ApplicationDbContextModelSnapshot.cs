﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Academic.Repository.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Academic.Repository.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Academic.Core.Base.IdentityUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(8)");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("varchar(13)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.ToTable("IdentityUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Academic.Core.Entities.EducationalPath", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(8)");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("MEDIUMTEXT");

                    b.Property<float>("Difficulty")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("float(2,2)")
                        .HasDefaultValue(1f);

                    b.Property<int>("InstructorId")
                        .HasColumnType("int(8)");

                    b.Property<string>("IntroductionBody")
                        .IsRequired()
                        .HasColumnType("LONGTEXT");

                    b.Property<int>("NumOfModules")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(4)")
                        .HasDefaultValue(1);

                    b.Property<int>("PathTaskId")
                        .HasColumnType("int(8)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("InstructorId");

                    b.HasIndex("PathTaskId");

                    b.ToTable("Paths");
                });

            modelBuilder.Entity("Academic.Core.Entities.ManyToManyEntities.ModuleSectionUsers", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int(8)");

                    b.Property<int>("ModuleSectionId")
                        .HasColumnType("int(8)");

                    b.Property<double>("ProgressPresented")
                        .HasColumnType("double");

                    b.Property<int>("UsersId")
                        .HasColumnType("int(8)");

                    b.HasKey("UserId", "ModuleSectionId");

                    b.HasIndex("ModuleSectionId");

                    b.HasIndex("UsersId");

                    b.ToTable("ModuleSectionUsers", (string)null);
                });

            modelBuilder.Entity("Academic.Core.Entities.ManyToManyEntities.ModuleUsers", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int(8)");

                    b.Property<int>("ModuleId")
                        .HasColumnType("int(8)");

                    b.Property<double>("ProgressPresented")
                        .HasColumnType("double");

                    b.Property<int>("UsersId")
                        .HasColumnType("int(8)");

                    b.HasKey("UserId", "ModuleId");

                    b.HasIndex("ModuleId");

                    b.HasIndex("UsersId");

                    b.ToTable("ModuleUsers", (string)null);
                });

            modelBuilder.Entity("Academic.Core.Entities.ManyToManyEntities.PathTaskUsers", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int(8)");

                    b.Property<int>("PathTaskId")
                        .HasColumnType("int(8)");

                    b.Property<bool>("HasCertification")
                        .HasColumnType("tinyint(1)");

                    b.Property<double>("Score")
                        .HasColumnType("double");

                    b.Property<int>("UsersId")
                        .HasColumnType("int(8)");

                    b.HasKey("UserId", "PathTaskId");

                    b.HasIndex("PathTaskId");

                    b.HasIndex("UsersId");

                    b.ToTable("PathTaskUsers", (string)null);
                });

            modelBuilder.Entity("Academic.Core.Entities.ManyToManyEntities.PathUsers", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int(8)");

                    b.Property<int>("PathId")
                        .HasColumnType("int(8)");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("NumberOfCompletedModules")
                        .HasColumnType("int");

                    b.Property<int>("PathsId")
                        .HasColumnType("int(8)");

                    b.Property<int>("UsersId")
                        .HasColumnType("int(8)");

                    b.HasKey("UserId", "PathId");

                    b.HasIndex("PathId");

                    b.HasIndex("PathsId");

                    b.HasIndex("UsersId");

                    b.ToTable("PathUsers", (string)null);
                });

            modelBuilder.Entity("Academic.Core.Entities.Module", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(8)");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(255)");

                    b.Property<float>("Difficulty")
                        .HasColumnType("float(1,1)");

                    b.Property<TimeSpan>("ExpectedTimeToComplete")
                        .HasColumnType("Time");

                    b.Property<int>("NumOfSections")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(4)")
                        .HasDefaultValue(1);

                    b.Property<int>("PathId")
                        .HasColumnType("int(8)");

                    b.Property<int>("PathId1")
                        .HasColumnType("int(8)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("PathId");

                    b.HasIndex("PathId1");

                    b.ToTable("Modules");
                });

            modelBuilder.Entity("Academic.Core.Entities.ModuleSection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(8)");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("LONGTEXT");

                    b.Property<int>("ModuleId")
                        .HasColumnType("int(8)");

                    b.Property<int>("QuizId")
                        .HasColumnType("int(8)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("ModuleId");

                    b.HasIndex("QuizId")
                        .IsUnique();

                    b.ToTable("ModuleSections");
                });

            modelBuilder.Entity("Academic.Core.Entities.MultiChoiceQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(8)");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasColumnType("char(2)");

                    b.Property<string>("ChoiceA")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("ChoiceB")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("ChoiceC")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("ChoiceD")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<int>("InstructorId")
                        .HasColumnType("int(8)");

                    b.Property<float>("Points")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("float(4,2)")
                        .HasDefaultValue(1f);

                    b.HasKey("Id");

                    b.HasIndex("InstructorId");

                    b.ToTable("MultiChoiceQuestions");
                });

            modelBuilder.Entity("Academic.Core.Entities.PathTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(8)");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("MEDIUMTEXT");

                    b.Property<float>("MinPointsToCertify")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("float(4,2)")
                        .HasDefaultValue(1f);

                    b.Property<int>("PathId")
                        .HasColumnType("int(8)");

                    b.Property<float>("TotalPoints")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("float(4,2)")
                        .HasDefaultValue(1f);

                    b.HasKey("Id");

                    b.HasIndex("PathId");

                    b.ToTable("PathTasks");
                });

            modelBuilder.Entity("Academic.Core.Entities.Quiz", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(8)");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("SectionId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Quizzes");
                });

            modelBuilder.Entity("Academic.Core.Entities.UserQuestionAnswer", b =>
                {
                    b.Property<int>("QuizId")
                        .HasColumnType("int(8)");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int(8)");

                    b.Property<int>("UserId")
                        .HasColumnType("int(8)");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserChoice")
                        .IsRequired()
                        .HasColumnType("varchar(1)");

                    b.HasKey("QuizId", "QuestionId", "UserId");

                    b.HasIndex("QuestionId");

                    b.HasIndex("UserId");

                    b.ToTable("userQuestionAnswers");
                });

            modelBuilder.Entity("PathTaskQuestions", b =>
                {
                    b.Property<int>("PathTaskId")
                        .HasColumnType("int(8)");

                    b.Property<int>("QuestionsId")
                        .HasColumnType("int(8)");

                    b.Property<int>("MultiChoiceQuestionId")
                        .HasColumnType("int(8)");

                    b.Property<int>("PathTaskId1")
                        .HasColumnType("int(8)");

                    b.HasKey("PathTaskId", "QuestionsId");

                    b.HasIndex("MultiChoiceQuestionId");

                    b.HasIndex("PathTaskId1");

                    b.HasIndex("QuestionsId");

                    b.ToTable("PathTaskQuestions");
                });

            modelBuilder.Entity("QuizQuestions", b =>
                {
                    b.Property<int>("QuestionsId")
                        .HasColumnType("int(8)");

                    b.Property<int>("QuizId")
                        .HasColumnType("int(8)");

                    b.Property<int>("MultiChoiceQuestionId")
                        .HasColumnType("int(8)");

                    b.Property<int>("QuizId1")
                        .HasColumnType("int(8)");

                    b.HasKey("QuestionsId", "QuizId");

                    b.HasIndex("MultiChoiceQuestionId");

                    b.HasIndex("QuizId");

                    b.HasIndex("QuizId1");

                    b.ToTable("QuizQuestions");
                });

            modelBuilder.Entity("Academic.Core.Identitiy.Admin", b =>
                {
                    b.HasBaseType("Academic.Core.Base.IdentityUser");

                    b.Property<string>("HashPassword")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("Academic.Core.Identitiy.Instructor", b =>
                {
                    b.HasBaseType("Academic.Core.Base.IdentityUser");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("JobType")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasDiscriminator().HasValue("Instructor");
                });

            modelBuilder.Entity("Academic.Core.Identitiy.User", b =>
                {
                    b.HasBaseType("Academic.Core.Base.IdentityUser");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(255)");

                    b.Property<float>("Points")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("float(4,2)")
                        .HasDefaultValue(0f);

                    b.ComplexProperty<Dictionary<string, object>>("Education", "Academic.Core.Identitiy.User.Education#EducationStatus", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("EducationalClass")
                                .IsRequired()
                                .HasColumnType("ENUM('First','Second','Third','Fourth','Fifth','Sixth','Seventh')");

                            b1.Property<string>("EducationalLevel")
                                .IsRequired()
                                .HasColumnType("ENUM('Secondary','Preparatory','Graduated','Undergraduate')");
                        });

                    b.HasDiscriminator().HasValue("User");
                });

            modelBuilder.Entity("Academic.Core.Entities.EducationalPath", b =>
                {
                    b.HasOne("Academic.Core.Identitiy.Instructor", "Instructor")
                        .WithMany("Paths")
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Academic.Core.Entities.PathTask", "PathTask")
                        .WithMany()
                        .HasForeignKey("PathTaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Instructor");

                    b.Navigation("PathTask");
                });

            modelBuilder.Entity("Academic.Core.Entities.ManyToManyEntities.ModuleSectionUsers", b =>
                {
                    b.HasOne("Academic.Core.Entities.ModuleSection", "ModuleSection")
                        .WithMany()
                        .HasForeignKey("ModuleSectionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Academic.Core.Identitiy.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Academic.Core.Identitiy.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ModuleSection");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Academic.Core.Entities.ManyToManyEntities.ModuleUsers", b =>
                {
                    b.HasOne("Academic.Core.Entities.Module", "Module")
                        .WithMany()
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Academic.Core.Identitiy.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Academic.Core.Identitiy.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Module");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Academic.Core.Entities.ManyToManyEntities.PathTaskUsers", b =>
                {
                    b.HasOne("Academic.Core.Entities.PathTask", "PathTask")
                        .WithMany()
                        .HasForeignKey("PathTaskId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Academic.Core.Identitiy.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Academic.Core.Identitiy.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PathTask");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Academic.Core.Entities.ManyToManyEntities.PathUsers", b =>
                {
                    b.HasOne("Academic.Core.Entities.EducationalPath", "Path")
                        .WithMany()
                        .HasForeignKey("PathId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Academic.Core.Entities.EducationalPath", null)
                        .WithMany()
                        .HasForeignKey("PathsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Academic.Core.Identitiy.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Academic.Core.Identitiy.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Path");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Academic.Core.Entities.Module", b =>
                {
                    b.HasOne("Academic.Core.Entities.EducationalPath", null)
                        .WithMany("Modules")
                        .HasForeignKey("PathId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Academic.Core.Entities.EducationalPath", "Path")
                        .WithMany()
                        .HasForeignKey("PathId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Path");
                });

            modelBuilder.Entity("Academic.Core.Entities.ModuleSection", b =>
                {
                    b.HasOne("Academic.Core.Entities.Module", "Module")
                        .WithMany("Sections")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Academic.Core.Entities.Quiz", "Quiz")
                        .WithOne("Section")
                        .HasForeignKey("Academic.Core.Entities.ModuleSection", "QuizId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Module");

                    b.Navigation("Quiz");
                });

            modelBuilder.Entity("Academic.Core.Entities.MultiChoiceQuestion", b =>
                {
                    b.HasOne("Academic.Core.Identitiy.Instructor", "Instructor")
                        .WithMany()
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Instructor");
                });

            modelBuilder.Entity("Academic.Core.Entities.PathTask", b =>
                {
                    b.HasOne("Academic.Core.Entities.EducationalPath", "Path")
                        .WithMany()
                        .HasForeignKey("PathId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Path");
                });

            modelBuilder.Entity("Academic.Core.Entities.UserQuestionAnswer", b =>
                {
                    b.HasOne("Academic.Core.Entities.MultiChoiceQuestion", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Academic.Core.Entities.Quiz", "Quiz")
                        .WithMany()
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Academic.Core.Identitiy.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");

                    b.Navigation("Quiz");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PathTaskQuestions", b =>
                {
                    b.HasOne("Academic.Core.Entities.MultiChoiceQuestion", null)
                        .WithMany()
                        .HasForeignKey("MultiChoiceQuestionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Academic.Core.Entities.PathTask", null)
                        .WithMany()
                        .HasForeignKey("PathTaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Academic.Core.Entities.PathTask", null)
                        .WithMany()
                        .HasForeignKey("PathTaskId1")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Academic.Core.Entities.MultiChoiceQuestion", null)
                        .WithMany()
                        .HasForeignKey("QuestionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("QuizQuestions", b =>
                {
                    b.HasOne("Academic.Core.Entities.MultiChoiceQuestion", null)
                        .WithMany()
                        .HasForeignKey("MultiChoiceQuestionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Academic.Core.Entities.MultiChoiceQuestion", null)
                        .WithMany()
                        .HasForeignKey("QuestionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Academic.Core.Entities.Quiz", null)
                        .WithMany()
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Academic.Core.Entities.Quiz", null)
                        .WithMany()
                        .HasForeignKey("QuizId1")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Academic.Core.Entities.EducationalPath", b =>
                {
                    b.Navigation("Modules");
                });

            modelBuilder.Entity("Academic.Core.Entities.Module", b =>
                {
                    b.Navigation("Sections");
                });

            modelBuilder.Entity("Academic.Core.Entities.Quiz", b =>
                {
                    b.Navigation("Section")
                        .IsRequired();
                });

            modelBuilder.Entity("Academic.Core.Identitiy.Instructor", b =>
                {
                    b.Navigation("Paths");
                });
#pragma warning restore 612, 618
        }
    }
}
