using Microsoft.EntityFrameworkCore;
using OrganizeYou.DAL.Entities;

namespace OrganizeYou.DAL.EF
{
    public class AppDbContext : DbContext
    {
        public DbSet<TaskObject> Tasks { get; set; }
        public DbSet<Status> Statuses { get; set; }

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
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Status>().HasData(new List<Status>
            {
                new Status { Id = 1, Name = "Не начата"},
                new Status { Id = 2, Name = "В процессе"},
                new Status { Id = 3, Name = "Приостановлена"},
                new Status { Id = 4, Name = "Завершена"},
            });
        }
    }
}
