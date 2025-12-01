using Microsoft.EntityFrameworkCore;

namespace Airbb.Models.DataLayer
{
    public class AirbbContext : DbContext
    {
        public AirbbContext(DbContextOptions<AirbbContext> options)
            : base(options) { }

        public DbSet<Location> Location { get; set; } = null!;
        public DbSet<Residence> Residence { get; set; } = null!;
        public DbSet<User> User { get; set; } = null!;
        public DbSet<Reservation> Reservation { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ConfigureLocations());
            modelBuilder.ApplyConfiguration(new ConfigureReservations());
            modelBuilder.ApplyConfiguration(new ConfigureResidences());
            modelBuilder.ApplyConfiguration(new ConfigureUsers());
        }

    }
}
