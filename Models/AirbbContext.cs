using Microsoft.EntityFrameworkCore;

namespace Airbb.Models
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
            modelBuilder.Entity<Location>().HasData(
                new Location { LocationId = 1, Name = "Seattle" },
                new Location { LocationId = 2, Name = "Denver" },
                new Location { LocationId = 3, Name = "Houston" },
                new Location { LocationId = 4, Name = "Orlando" }
            );

            modelBuilder.Entity<Residence>().HasData(
                new Residence
                {
                    ResidenceId = 1,
                    Name = "Seattle Skyline Silhouette",
                    ResidencePicture = "SeattleSkylineSilhouette.png",
                    LocationId = 1,
                    GuestNumber = 2,
                    BedroomNumber = 1,
                    BathroomNumber = 1,
                    PricePerNight = "130"
                },
                new Residence
                {
                    ResidenceId = 2,
                    Name = "Denver Mountain Cabin",
                    ResidencePicture = "DenverMountainCabin.png",
                    LocationId = 2,
                    GuestNumber = 6,
                    BedroomNumber = 3,
                    BathroomNumber = 2,
                    PricePerNight = "150"
                },
                new Residence
                {
                    ResidenceId = 3,
                    Name = "Houston Downtown Loft",
                    ResidencePicture = "HoustonDowntownLoft.png",
                    LocationId = 3,
                    GuestNumber = 4,
                    BedroomNumber = 2,
                    BathroomNumber = 2,
                    PricePerNight = "95"
                },
                new Residence
                {
                    ResidenceId = 4,
                    Name = "Orlando Family Villa",
                    ResidencePicture = "OrlandoFamilyVilla.png",
                    LocationId = 4,
                    GuestNumber = 8,
                    BedroomNumber = 4,
                    BathroomNumber = 3,
                    PricePerNight = "170"
                }
            );

            modelBuilder.Entity<Reservation>().HasData(
                new Reservation
                {
                    ReservationId = 1,
                    ResidenceId = 1,
                    ReservationStartDate = new DateTime(2025, 12, 5),
                    ReservationEndDate = new DateTime(2025, 12, 8)
                },
                new Reservation
                {
                    ReservationId = 2,
                    ResidenceId = 2,
                    ReservationStartDate = new DateTime(2025, 12, 10),
                    ReservationEndDate = new DateTime(2025, 12, 15)
                },
                new Reservation
                {
                    ReservationId = 3,
                    ResidenceId = 3,
                    ReservationStartDate = new DateTime(2025, 12, 20),
                    ReservationEndDate = new DateTime(2025, 12, 25)
                }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    Name = "Lucas Bennett",
                    PhoneNo = "955-707-8080",
                    EmailAddress = "lucas.bennett@gmail.com",
                    DOB = "02/19/1997"
                },
                new User
                {
                    UserId = 2,
                    Name = "Isabella Perez",
                    PhoneNo = "201-909-1010",
                    EmailAddress = "isabella.perez@gmail.com",
                    DOB = "06/23/2000"
                },
                new User
                {
                    UserId = 3,
                    Name = "Ethan Clark",
                    PhoneNo = "614-111-2121",
                    EmailAddress = "ethan.clark@gmail.com",
                    DOB = "10/14/1999"
                },
                new User
                {
                    UserId = 4,
                    Name = "Jim Greevy",
                    PhoneNo = "216-090-6767",
                    EmailAddress = "jim.greevy@gmail.com",
                    DOB = "12/12/1999"
                }
            );
        }

    }
}
