using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airbb.Models
{
    internal class ConfigureLocations : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> entity)
        {
            // seed initial data
            entity.HasData(
                new Location { LocationId = 1, Name = "Seattle" },
                new Location { LocationId = 2, Name = "Denver" },
                new Location { LocationId = 3, Name = "Houston" },
                new Location { LocationId = 4, Name = "Orlando" }
            );
        }
    }

}