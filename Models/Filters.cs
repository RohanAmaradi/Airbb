namespace Airbb.Models
{
    public class Filters
    {
        public Filters(string filterstring)
        {
            FilterString = filterstring ?? "all-all-all-all";
            string[] filters = FilterString.Split('-');
            LocationId = filters[0];
            CheckInDate = filters[1];
            CheckOutDate = filters[2];
            NoOfGuests = filters[3];
        }
        public string FilterString { get; }
        public string NoOfGuests { get; }
        public string CheckInDate { get; }
        public string CheckOutDate { get; }
        public string LocationId { get; }

        public bool HasNoOfGuests => NoOfGuests.ToString().ToLower() != "all";
        public bool HasCheckInDate => CheckInDate.ToString().ToLower() != "all";
        public bool HasCheckOutDate => CheckOutDate.ToString().ToLower() != "all";
        public bool HasLocation => LocationId.ToLower() != "all";
    }
}
