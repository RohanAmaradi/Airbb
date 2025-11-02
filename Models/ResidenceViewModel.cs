namespace Airbb.Models
{
    public class ResidenceViewModel
    {
        public string ActiveLocation { get; set; } = "all";
        public string ActiveCheckInDate { get; set; } = "all";
        public string ActiveCheckOutDate { get; set; } = "all";
        public string ActiveNoOfGuests { get; set; } = "all";
        public List<Location> Location { get; set; } = new List<Location>();
        public List<Reservation> Reservation { get; set; } = new List<Reservation>();
        public List<Residence> Residence { get; set; } = new List<Residence>();
        public Residence Residences { get; set; } = new Residence();

        public string CheckActiveLocation(string d) =>
            d.ToLower() == ActiveLocation.ToLower() ? "active" : "";
    }
}
