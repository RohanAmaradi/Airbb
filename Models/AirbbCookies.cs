namespace Airbb.Models
{
    public class AirbbCookies
    {
        private const string ReservationKey = "Reservations";
        private const string Delimiter = "-";
        private readonly IRequestCookieCollection _requestCookies;
        private readonly IResponseCookies _responseCookies;

        public AirbbCookies(IRequestCookieCollection request, IResponseCookies response)
        {
            _requestCookies = request ?? throw new ArgumentNullException(nameof(request));
            _responseCookies = response ?? throw new ArgumentNullException(nameof(response));
        }

        public void SetReservationIds(List<Reservation> reservations)
        {
            var ids = reservations.Select(r => r.ReservationId.ToString());
            SetReservationIds(ids);
        }

        public void SetReservationIds(IEnumerable<string> ids)
        {
            var idsString = string.Join(Delimiter, ids);
            var options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(30),
                IsEssential = true
            };

            _responseCookies.Append(ReservationKey, idsString, options);
        }

        public string[] GetReservationIds()
        {
            var cookie = _requestCookies[ReservationKey];
            return string.IsNullOrEmpty(cookie)
                ? Array.Empty<string>()
                : cookie.Split(Delimiter);
        }
        public void RemoveReservationId(int id)
        {
            var ids = GetReservationIds();
            var latestIds = ids.Where(lid => lid != id.ToString()).ToArray();
            SetReservationIds(latestIds);
        }
    }
}
