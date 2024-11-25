using Academic.Core.Entities;
using Academic.Core.Identitiy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Academic.Repository.Data.Configurations
{
    public class PathConfig : IEntityTypeConfiguration<EducationalPath>
    {
        public void Configure(EntityTypeBuilder<EducationalPath> builder)
        {
            //   Key
            builder.Property(m => m.Id)
               .ValueGeneratedOnAdd()
               .HasColumnType("int(8)")
               .UseMySqlIdentityColumn();
            builder.HasKey(c => c.Id);


            // Properties
            builder.Property(p => p.Title).IsRequired()
                .HasColumnType("varchar(255)");

            builder.Property(p => p.Description)
                .HasColumnType("MEDIUMTEXT");

            builder.Property(p => p.Difficulty).IsRequired()
                   .HasDefaultValue(1)
                   .HasColumnType("float(2,2)");

            builder.Property(p => p.NumOfModules).IsRequired()
                   .HasDefaultValue(1)
                   .HasColumnType("int(4)");

            builder.Property(p => p.CreatedAt)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(p => p.IntroductionBody)
                .HasColumnType("LONGTEXT")
                .IsRequired();


            // Relation
            builder.HasMany(p => p.Modules)
                  .WithOne()
                  .HasForeignKey(m => m.PathId)
                  .IsRequired()
                  .OnDelete(DeleteBehavior.Restrict);


           

        }
    }
}

