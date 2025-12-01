using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airbb.Models
{
    internal class ConfigureReservations : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> entity)
        {
            // seed initial data
            entity.HasData(
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
        }
    }

}