using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GroupEntity = DataAccess.Entities.Group;

namespace DataAccess
{
    public class TerminContext : DbContext
    {
        public TerminContext()
        {
        }
        public TerminContext(DbContextOptions<TerminContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var groups = new List<GroupEntity>
            {
                new GroupEntity { Id = 1, Name = "Admin" },
                new GroupEntity { Id = 2, Name = "User" },
                new GroupEntity { Id = 3, Name = "Guest" }
            };
            modelBuilder.Entity<GroupEntity>().HasData(groups);
            modelBuilder.ApplyConfiguration(new Configurations.GroupConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.UserConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.ProductConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.OrderConfiguration());

            modelBuilder.Entity<Entities.Group>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Entities.User>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Entities.Product>().HasQueryFilter(e => !e.IsDeleted);

            modelBuilder.Entity<Entities.OrderProduct>().HasKey(op => new { op.OrderId, op.ProductId });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=desktop-sn7nfh5\sqlexpress;Initial Catalog=TerminDb;Integrated Security=True;Trust Server Certificate=True");
            }
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Entities.Entity e)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            e.CreatedAt = DateTime.Now;
                            e.IsDeleted = false;
                            e.isActive = true;
                            e.ModifiedAt = null;
                            e.DeletedAt = null;
                            break;
                        case EntityState.Modified:
                            e.ModifiedAt = DateTime.Now;
                            break;
                        /*case EntityState.Deleted:
                            entry.State = EntityState.Modified;
                            e.IsDeleted = true;
                            e.DeletedAt = DateTime.Now;
                            break;*/
                    }
                }
            }
            return base.SaveChanges();
        }
        public DbSet<DataAccess.Entities.Group> Groups { get; set; }
        public DbSet<DataAccess.Entities.User> Users { get; set; }
        public DbSet<DataAccess.Entities.Product> Products { get; set; }
        public DbSet<DataAccess.Entities.Order> Orders { get; set; }
    }
}
