using System.ComponentModel.DataAnnotations;

namespace Airbb.Models
{
    public class Location
    {
        public int LocationId { get; set; }

        [Required(ErrorMessage = "Please enter a Name.")]
        public string Name { get; set; } = string.Empty;
    }
}
