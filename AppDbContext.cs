using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pract12_TRPO
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<InterestGroup> InterestGroups { get; set; }
        public DbSet<UserInterestGroup> UserInterestGroups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-SHKSOAD\\SQLEXPRESS;Database=Pract12;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>() // связь один к одному
                .HasOne(s => s.UserProfile)
                .WithOne(ps => ps.Student)
                .HasForeignKey<UserProfile>(ps => ps.StudentId);

            modelBuilder.Entity<Student>() // связь один ко многим
                .HasOne(s => s.Role)
                .WithMany(r => r.Students)
                .HasForeignKey(s => s.RoleId);

            base.OnModelCreating(modelBuilder);

            // многие ко многим

            modelBuilder.Entity<UserInterestGroup>() // составной первичный ключ
                .HasKey(uig => new { uig.UserId, uig.InterestGroupId });

            modelBuilder.Entity<UserInterestGroup>() // связь со студентом
                .HasOne(uig => uig.Student)
                .WithMany(s => s.UserInterestGroups)
                .HasForeignKey(uig => uig.UserId);

            modelBuilder.Entity<UserInterestGroup>() // связь с группой интересов
                .HasOne(uig => uig.InterestGroup)
                .WithMany(g => g.UserInterestGroups)
                .HasForeignKey(uig => uig.InterestGroupId);

            modelBuilder.Entity<InterestGroup>() // уникальный индекс для группы
                .HasIndex(g => g.Title)
                .IsUnique();
        }
    }
}
