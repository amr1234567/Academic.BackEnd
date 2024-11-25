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
    public class ModuleConfig : IEntityTypeConfiguration<Module>
    {
        void IEntityTypeConfiguration<Module>.Configure(EntityTypeBuilder<Module> builder)
        {
            // Key
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id)
               .ValueGeneratedOnAdd()
               .HasColumnType("int(8)")
               .UseMySqlIdentityColumn();

            builder.Property(m => m.Title).IsRequired()
                .HasColumnType("varchar(255)");  //for mysql data types

            builder.Property(m => m.Difficulty)
                .HasColumnType("float(1,1)")   //for mysql data types
                .IsRequired();

            builder.Property(m => m.Description)
                .HasColumnType("varchar(255)"); //for mysql data types

            builder.Property(m => m.NumOfSections)
                .HasColumnType("int(2)")  //for mysql data types
                .IsRequired()
                .HasDefaultValue(1);

            builder.Property(m => m.ExpectedTimeToComplete)
                   .IsRequired()
                   .HasColumnType("Time");  //for mysql data types

            builder.Property(m => m.CreatedAt).IsRequired()
                .HasColumnType("datetime");  //for mysql data types
            builder.Property(m => m.NumOfSections)
                .IsRequired()
                .HasColumnType("int(4)");  //for mysql data types

            // Relations
            builder.HasMany(m => m.Sections)
                   .WithOne(s => s.Module)
                   .HasForeignKey(s => s.ModuleId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.Path)
                .WithMany()
                .HasForeignKey(m => m.PathId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
