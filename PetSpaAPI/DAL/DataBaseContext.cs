using Microsoft.EntityFrameworkCore;
using PetSpaAPI.DAL.Entities;

namespace PetSpaAPI.DAL
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }

        public DbSet<Charge> Charges { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Species> Species { get; set; }
        public DbSet<Breed> Breeds { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Charge>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Employee>().HasIndex(e => e.Cc).IsUnique();
            modelBuilder.Entity<Service>().HasIndex(s => s.Name).IsUnique();
            modelBuilder.Entity<Client>().HasIndex(c => c.Cc).IsUnique();
            modelBuilder.Entity<Species>().HasIndex(s => s.Id).IsUnique();
            modelBuilder.Entity<Breed>().HasIndex(b => b.Id).IsUnique();
            modelBuilder.Entity<Pet>().HasIndex(p => p.Id).IsUnique();
            modelBuilder.Entity<Appointment>().HasIndex(p => p.Id).IsUnique();
        }
    }
}
