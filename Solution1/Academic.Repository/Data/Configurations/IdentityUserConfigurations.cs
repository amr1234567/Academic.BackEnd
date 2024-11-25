using Academic.Core.Base;
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
    internal class IdentityUserConfigurations : IEntityTypeConfiguration<IdentityUser>
    {
        public void Configure(EntityTypeBuilder<IdentityUser> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(m => m.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("int(8)")
                .UseMySqlIdentityColumn();

            builder.ToTable("IdentityUsers");
            builder.Property(p => p.UserName).IsRequired().HasColumnType("varchar(255)");

            builder.Property(p => p.Password).IsRequired().HasColumnType("varchar(255)");

            builder.Property(p => p.Email).IsRequired()
                .HasColumnType("varchar(255)");

            builder.Property(p => p.Phone).IsRequired()
                .HasColumnType("varchar(20)");
        }
    }
}
