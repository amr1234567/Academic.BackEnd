using Academic.Core.Identitiy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Repository.Data.Configurations
{
    public class InstructorConfig : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            builder.Property(i => i.HashedPassword).HasColumnType("varchar(255)").IsRequired();

            builder.Property(i => i.JobType).IsRequired().HasColumnType("varchar(255)").HasMaxLength(100);
        }
    }
}
