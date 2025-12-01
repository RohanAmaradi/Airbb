using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airbb.Models
{
    internal class ConfigureResidences : IEntityTypeConfiguration<Residence>
    {
        public void Configure(EntityTypeBuilder<Residence> entity)
        {
            // seed initial data
            entity.HasData(
                new Residence
                {
                    ResidenceId = 1,
                    Name = "Seattle Skyline Silhouette",
                    ResidencePicture = "SeattleSkylineSilhouette.png",
                    LocationId = 1,
                    GuestNumber = 2,
                    BedroomNumber = 1,
                    BathroomNumber = 1,
                    PricePerNight = "130",
                    BuiltYear = new DateTime(1899, 05, 07),
                    UserId = 1,
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
                    PricePerNight = "150",
                    BuiltYear = new DateTime(1901, 05, 05),
                    UserId = 4,
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
                    PricePerNight = "95",
                    BuiltYear = new DateTime(2001, 09, 07),
                    UserId = 3,
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
                    PricePerNight = "170",
                    BuiltYear = new DateTime(1989, 12, 05),
                    UserId = 2,
                }
            );
        }
    }

}