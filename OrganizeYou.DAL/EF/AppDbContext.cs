using Microsoft.EntityFrameworkCore;
using OrganizeYou.DAL.Entities;

namespace OrganizeYou.DAL.EF
{
    public class AppDbContext : DbContext
    {
        public DbSet<TaskObject> Tasks { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "admin";
            string userRoleName = "user";

            string adminEmail = "admin@mail.ru";
            string adminPassword = "testAdmin2209";

            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role userRole = new Role { Id = 2, Name = userRoleName };
            User adminUser = new User { Id = 1, Email = adminEmail, Password = adminPassword, RoleId = adminRole.Id };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser });

            modelBuilder.Entity<Status>().HasData(new List<Status>
            {
                new Status { Id = 1, Name = "Не начата"},
                new Status { Id = 2, Name = "В процессе"},
                new Status { Id = 3, Name = "Приостановлена"},
                new Status { Id = 4, Name = "Завершена"},
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
