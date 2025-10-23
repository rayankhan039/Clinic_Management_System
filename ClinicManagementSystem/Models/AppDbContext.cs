using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ClinicManagementSystem.Models
{
    public class AppDbContext : DbContext
    {
       public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>().Property(d => d.IsActive).HasDefaultValue(false);

            modelBuilder.Entity<Appointment>().Property(a => a.TokenNumber).HasMaxLength(10);
        }
    }
}
