using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Airbb.Models
{
    internal class ConfigureUsers : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            // seed initial data
            entity.HasData(
                new User
                {
                    UserId = 1,
                    Name = "Lucas Bennett",
                    PhoneNo = "955-707-8080",
                    EmailAddress = "lucas.bennett@gmail.com",
                    DOB = new DateTime(1997, 02, 19),
                    SSN = "124-866-6878",
                    UserType = "Owner"
                },
                new User
                {
                    UserId = 2,
                    Name = "Isabella Perez",
                    PhoneNo = "201-909-1010",
                    EmailAddress = "isabella.perez@gmail.com",
                    DOB = new DateTime(2000, 06, 23),
                    SSN = "421-897-4356",
                    UserType = "Client"
                },
                new User
                {
                    UserId = 3,
                    Name = "Ethan Clark",
                    PhoneNo = "614-111-2121",
                    EmailAddress = "ethan.clark@gmail.com",
                    DOB = new DateTime(1999, 10, 14),
                    SSN = "124-409-6780",
                    UserType = "Admin"
                },
                new User
                {
                    UserId = 4,
                    Name = "Jim Greevy",
                    PhoneNo = "216-090-6767",
                    EmailAddress = "jim.greevy@gmail.com",
                    DOB = new DateTime(1999, 12, 12),
                    SSN = "989-456-4567",
                    UserType = "Owner"
                }
            );
        }
    }

}