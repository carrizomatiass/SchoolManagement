using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Infrastructure.Data.Configurations
{
    public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.DocumentNumber)
                .HasMaxLength(50);

            builder.Property(p => p.Phone)
                .HasMaxLength(50);

            builder.Property(p => p.Address)
                .HasMaxLength(500);

            builder.Property(p => p.AvatarUrl)
                .HasMaxLength(500);

            builder.Ignore(p => p.FullName);
        }
    }
}
