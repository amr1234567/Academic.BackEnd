using Academic.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Repository.Data.Configurations
{
    public class MultiChoiceQuestionConfig : IEntityTypeConfiguration<MultiChoiceQuestion>
    {
        public void Configure(EntityTypeBuilder<MultiChoiceQuestion> builder)
        {
            builder.Property(m => m.Id)
                 .ValueGeneratedOnAdd()
                 .HasColumnType("int(8)")
                 .UseMySqlIdentityColumn();
            builder.HasKey(c => c.Id);



            builder.Property(mcq => mcq.Content).HasColumnType("varchar(200)").IsRequired();

            builder.Property(mcq => mcq.ChoiceA).HasColumnType("varchar(200)").IsRequired();

            builder.Property(mcq => mcq.ChoiceB).HasColumnType("varchar(200)").IsRequired();

            builder.Property(mcq => mcq.ChoiceC).HasColumnType("varchar(200)").IsRequired();

            builder.Property(mcq => mcq.ChoiceD).HasColumnType("varchar(200)").IsRequired();

            builder.Property(mcq => mcq.Answer).HasColumnType("char(2)").IsRequired();


            // Relation
            builder.Property(mcq => mcq.Points)
                   .IsRequired()
                   .HasDefaultValue(1)
                   .HasColumnType("float(4,2)");
        }
    }

}
