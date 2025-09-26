using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
        {
            builder.HasAlternateKey(u => u.Username);
            builder.Property(u => u.Username)
                   .IsRequired()
                   .HasMaxLength(50);
            builder.Property(u => u.FirstName)
                   .IsRequired()
                   .HasMaxLength(50);
            builder.Property(u => u.LastName)
                     .IsRequired()
                     .HasMaxLength(50);
            builder.HasMany(u => u.Orders)
                   .WithOne(o => o.User)
                   .HasForeignKey(o => o.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
