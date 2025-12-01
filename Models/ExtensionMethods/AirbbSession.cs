namespace Airbb.Models.ExtensionMethods
{
    public class AirbbSession
    {
        private const string ReservationKey = "Reservations";
        private const string CountKey = "reservationsCount";
        private const string LocationKey = "residenceLocation";
        private const string CheckInDateKey = "residenceCheckInDate";
        private const string CheckOutDateKey = "residenceCheckOutDate";
        private const string NoOfGuestsKey = "residenceNoOfGuests";

        private ISession session { get; set; }
        public AirbbSession(ISession session) => this.session = session;

        public void SetReservations(List<Reservation> reservations)
        {
            session.SetObject(ReservationKey, reservations);
            session.SetInt32(CountKey, reservations.Count);
        }
        public List<Reservation> GetReservations() =>
            session.GetObject<List<Reservation>>(ReservationKey) ?? new List<Reservation>();
        public int? GetReservationCount() => session.GetInt32(CountKey);

        public void SetActiveLocation(string activeLocation) =>
            session.SetString(LocationKey, activeLocation);
        public string GetActiveLocation() =>
            session.GetString(LocationKey) ?? string.Empty;

        public void SetActiveCheckInDate(string activeCheckInDate) =>
            session.SetString(CheckInDateKey, activeCheckInDate);
        public string GetActiveCheckInDate() =>
            session.GetString(CheckInDateKey) ?? string.Empty;

        public void SetActiveCheckOutDate(string activeCheckOutDate) =>
            session.SetString(CheckOutDateKey, activeCheckOutDate);
        public string GetActiveCheckOutDate() =>
            session.GetString(CheckOutDateKey) ?? string.Empty;

        public void SetActiveNoOfGuests(string activeNoOfGuests) =>
            session.SetString(NoOfGuestsKey, activeNoOfGuests);
        public string GetActiveNoOfGuests() =>
            session.GetString(NoOfGuestsKey) ?? string.Empty;
    }
}
