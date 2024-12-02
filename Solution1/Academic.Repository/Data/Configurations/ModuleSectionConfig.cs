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
    public class ModuleSectionConfig : IEntityTypeConfiguration<ModuleSection>
    {
        public void Configure(EntityTypeBuilder<ModuleSection> builder)
        {
            builder.Property(m => m.Id)
               .ValueGeneratedOnAdd()
               .HasColumnType("int(8)")
               .UseMySqlIdentityColumn();
            builder.HasKey(c => c.Id);

            builder.Property(m => m.Title).IsRequired()
                .HasColumnType("varchar(255)"); //for mysql data types

            builder.Property(m => m.Body).IsRequired()
                .HasColumnType("LONGTEXT");  //for mysql data types

            builder.HasOne(m => m.Quiz)
            .WithOne(q => q.Section)
            .HasForeignKey<ModuleSection>(m => m.QuizId)
            .IsRequired(false) 
            .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
