using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configurations
{
    internal class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.Property(g => g.Name)
                   .IsRequired()
                   .HasMaxLength(30);
            builder.HasIndex(g => g.Name)
                   .IsUnique();
            builder.HasMany(g => g.Users)
                   .WithOne(u => u.Group)
                   .HasForeignKey(u => u.GroupId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
