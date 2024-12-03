using Academic.Core.Entities.ManyToManyEntities;
using Academic.Core.Enums;
using Academic.Core.Identitiy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Academic.Repository.Data.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        void IEntityTypeConfiguration<User>.Configure(EntityTypeBuilder<User> builder)
        {
            // Key
          
            builder.ComplexProperty(u => u.Education, e =>
            {
                e.Property(p => p.EducationalClass)
                    .HasConversion(
                        data => data.ToString(),
                        data => (EducationalClass)Enum.Parse(typeof(EducationalClass), data))
                   .HasColumnType($"ENUM('{EducationalClass.First.ToString()}','{EducationalClass.Second.ToString()}','{EducationalClass.Third.ToString()}','{EducationalClass.Fourth.ToString()}','{EducationalClass.Fifth.ToString()}','{EducationalClass.Sixth.ToString()}','{EducationalClass.Seventh.ToString()}')");

                e.Property(p => p.EducationalLevel)
                   .HasConversion(
                       data => data.ToString(),
                       data => (EducationalLevel)Enum.Parse(typeof(EducationalLevel), data))
                   .HasColumnType($"ENUM('{EducationalLevel.Secondary.ToString()}','{EducationalLevel.Preparatory.ToString()}','{EducationalLevel.Graduated.ToString()}','{EducationalLevel.Undergraduate.ToString()}')");
            });

            

            builder.Property(u => u.Country).IsRequired()
               .HasColumnType("varchar(255)");

            builder.Property(u => u.Points)
                    .HasColumnType("float(4,2)")
                    .IsRequired()
                    .HasDefaultValue(0);

            //Relation

            builder.HasMany(u => u.Paths)
                  .WithMany(p => p.Users)
                  .UsingEntity<PathUsers>(join =>
                  {
                      join.HasOne(j => j.User)
                          .WithMany()
                          .HasForeignKey(u => u.UserId)
                          .OnDelete(DeleteBehavior.Restrict);
                      join.HasOne(j => j.Path)
                          .WithMany()
                          .HasForeignKey(u => u.PathId)
                          .OnDelete(DeleteBehavior.Restrict);
                      join.ToTable("PathUsers");
                      join.HasKey(j => new { j.UserId, j.PathId });
                  }); 

            
            builder.HasMany(u => u.Modules)
                   .WithMany(m => m.Users)
                   .UsingEntity<ModuleUsers>(join =>
                    {
                        join.HasOne(j => j.User)
                            .WithMany()
                            .HasForeignKey(j => j.UserId)
                            .OnDelete(DeleteBehavior.Restrict);
                        join.HasOne(j => j.Module)
                            .WithMany()
                            .HasForeignKey(j => j.ModuleId)
                            .OnDelete(DeleteBehavior.Restrict);
                        join.ToTable("ModuleUsers");
                        join.HasKey(j => new { j.UserId, j.ModuleId });
                    });

            builder.HasMany(u => u.ModuleSections)
                  .WithMany(m => m.Users)
                  .UsingEntity<ModuleSectionUsers>(join =>
                  {
                      join.HasOne(j => j.User)
                           .WithMany()
                           .HasForeignKey(j => j.UserId)
                           .OnDelete(DeleteBehavior.Restrict);
                      join.HasOne(j => j.ModuleSection)
                           .WithMany()
                           .HasForeignKey(j => j.ModuleSectionId)
                           .OnDelete(DeleteBehavior.Restrict);
                      join.ToTable("ModuleSectionUsers");

                      join.HasKey(j => new { j.UserId, j.ModuleSectionId });
                  });


            builder.HasMany(u => u.PathTasks)
                .WithMany(p => p.Users)
                .UsingEntity<PathTaskUsers>(join =>
                {
                    join.HasOne(j => j.PathTask)
                       .WithMany()
                       .HasForeignKey(j => j.PathTaskId)
                       .OnDelete(DeleteBehavior.Restrict);
                    join.HasOne(j => j.User)
                        .WithMany()
                        .HasForeignKey(j => j.UserId)
                        .OnDelete(DeleteBehavior.Restrict);
                    join.ToTable("PathTaskUsers");

                    join.HasKey(j => new { j.UserId, j.PathTaskId });
                });

        }
    }
}
