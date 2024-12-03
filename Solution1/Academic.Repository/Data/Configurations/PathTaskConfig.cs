using Academic.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Repository.Data.Configurations
{
    public class PathTaskConfig : IEntityTypeConfiguration<PathTask>
    {
        public void Configure(EntityTypeBuilder<PathTask> builder)
        {
            builder.Property(m => m.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("int(8)")
                .UseMySqlIdentityColumn();
            builder.HasKey(c => c.Id);


            builder.Property(p => p.TotalPoints)
                .IsRequired()
                .HasColumnType("float(4,2)")
                .HasDefaultValue(1);

            builder.Property(p => p.MinPointsToCertify)
                .IsRequired()
                .HasColumnType("float(4,2)")
                .HasDefaultValue(1);

            builder.Property(p => p.Description)
                .IsRequired()
                .HasColumnType("MEDIUMTEXT")
                .HasMaxLength(200);


            // Relation
            builder.HasMany(p => p.Questions)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                   "PathTaskQuestions", // Name of the join table
                   join =>
                   {
                       join.HasOne<PathTask>() // The `User` side of the relationship
                           .WithMany()
                           .IsRequired()
                           .HasForeignKey("PathTaskId")
                           .OnDelete(DeleteBehavior.Restrict); // Restrict delete on Users
                       join.HasOne<MultiChoiceQuestion>() // The `Path` side of the relationship
                           .WithMany()
                           .IsRequired()
                           .HasForeignKey("QuestionId")
                           .OnDelete(DeleteBehavior.Restrict);
                   });
        }
    }
}
