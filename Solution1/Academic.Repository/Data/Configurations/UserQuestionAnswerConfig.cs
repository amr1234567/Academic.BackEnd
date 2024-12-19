using Academic.Core.Entities;
using Academic.Core.Identitiy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Repository.Data.Configurations
{
    public class UserQuestionAnswerConfig : IEntityTypeConfiguration<UserQuestionAnswer>
    {
        public void Configure(EntityTypeBuilder<UserQuestionAnswer> builder)
        {
            builder.HasKey(u => new { u.QuizId, u.QuestionId, u.UserId });
            builder
               .HasOne(uqa => uqa.User)
               .WithMany()
               .HasForeignKey(uqa => uqa.UserId);

            builder
                .HasOne(uqa => uqa.Quiz)
                .WithMany()
                .HasForeignKey(uqa => uqa.QuizId)
                .IsRequired(false);

            builder
                .HasOne(uqa => uqa.PathTask)
                .WithMany()
                .HasForeignKey(uqa => uqa.PathTaskId)
                .IsRequired(false);

            builder
                .HasOne(uqa => uqa.Question)
                .WithMany(q=> q.UsersAnswers)
                .HasForeignKey(uqa => uqa.QuestionId);
        }
    }
}
