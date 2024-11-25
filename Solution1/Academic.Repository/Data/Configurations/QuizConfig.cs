using Academic.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Repository.Data.Configurations
{
    public class QuizConfig : IEntityTypeConfiguration<Quiz>
    {
        public void Configure(EntityTypeBuilder<Quiz> builder)
        {
            builder.Property(m => m.Id)
               .ValueGeneratedOnAdd()
               .HasColumnType("int(8)")
               .UseMySqlIdentityColumn();
            builder.HasKey(c => c.Id);


            builder.Property(p => p.Title).IsRequired()
               .HasColumnType("varchar(255)");

            // Relation

            builder.HasMany(p => p.Questions)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "QuizQuestions", // Name of the join table
                    join =>
                    {
                        join.HasOne<Quiz>() // The `User` side of the relationship
                            .WithMany()
                            .IsRequired()
                            .OnDelete(DeleteBehavior.Restrict); // Restrict delete on Users
                        join.HasOne<MultiChoiceQuestion>() // The `Path` side of the relationship
                            .WithMany()
                            .IsRequired()
                            .OnDelete(DeleteBehavior.Restrict);
                        
                    });
        }
    }
}
