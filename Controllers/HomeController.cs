using Airbb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Airbb.Controllers
{
    public class HomeController : Controller
    {

        private AirbbContext _context;
        private AirbbSession _session => new AirbbSession(HttpContext.Session);
        private AirbbCookies _cookies => new AirbbCookies(Request.Cookies, Response.Cookies);
        public HomeController(AirbbContext context)
        {
            _context = context;
        }

        public ViewResult Index(ResidenceViewModel model)
        {
            var filterList = new Filters($"{model.ActiveLocation}-{DateTime.TryParse(model.ActiveCheckInDate, out DateTime checkInDate)}" +
                $"-{DateTime.TryParse(model.ActiveCheckOutDate, out DateTime checkOutDate)}-{model.ActiveNoOfGuests}");
            var filterString = new Filters(filterList.FilterString);
            _session.SetActiveLocation(model.ActiveLocation);
            _session.SetActiveCheckInDate(model.ActiveCheckInDate);
            _session.SetActiveCheckOutDate(model.ActiveCheckOutDate);
            _session.SetActiveNoOfGuests(model.ActiveNoOfGuests);
            int? count = _session.GetReservationCount();
            if (!count.HasValue)
            {
                string[] ids = _cookies.GetReservationIds();
                if (ids.Length > 0)
                {
                    var reservations = _context.Reservation.Include(r => r.Residence).ThenInclude(res => res.Location)
                        .Where(r => ids.Contains(r.ReservationId.ToString()))
                        .ToList();
                    _session.SetReservations(reservations);
                }
            }
            model.Location = _context.Location.OrderBy(l => l.Name).ToList();
            IQueryable<Residence> query = _context.Residence
                .Include(r => r.Location)
                .OrderBy(r => r.Name);
            if (filterString.HasCheckInDate && filterString.HasCheckOutDate)
            {
                var reservedResidenceIds = _context.Reservation
                    .Where(res =>
                        res.ReservationStartDate <= checkOutDate &&
                        res.ReservationEndDate >= checkInDate)
                    .Select(res => res.ResidenceId)
                    .Distinct()
                    .ToList();

                query = query.Where(r => !reservedResidenceIds.Contains(r.ResidenceId));
            }
            if (filterString.HasLocation)
            {
                query = query.Where(r => r.Location.LocationId.ToString() == model.ActiveLocation);
            }
            if (filterString.HasNoOfGuests)
            {
                if (int.TryParse(model.ActiveNoOfGuests, out int guests))
                {
                    query = query.Where(r => r.GuestNumber == guests);
                }
            }
            model.Residence = query.ToList();
            return View(model);
        }
        public IActionResult Reservations()
        {
            var reservationIds = _cookies.GetReservationIds();
            var reservations = _context.Reservation.Include(r => r.Residence).ThenInclude(res => res.Location)
                .Where(r => reservationIds.Contains(r.ReservationId.ToString()))
                .ToList();
            var model = new ResidenceViewModel
            {
                Reservation = reservations,
                ActiveLocation = _session.GetActiveLocation(),
                ActiveCheckInDate = _session.GetActiveCheckInDate(),
                ActiveCheckOutDate = _session.GetActiveCheckOutDate(),
                ActiveNoOfGuests = _session.GetActiveNoOfGuests()
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Reserve(int id)
        {
            const string format = "MM/dd/yyyy";
            var culture = System.Globalization.CultureInfo.InvariantCulture;

            DateTime.TryParseExact(_session.GetActiveCheckInDate(), format, culture, System.Globalization.DateTimeStyles.None, out DateTime checkIn);
            DateTime.TryParseExact(_session.GetActiveCheckOutDate(), format, culture, System.Globalization.DateTimeStyles.None, out DateTime checkOut);

            if (checkIn == default) checkIn = DateTime.Today;
            if (checkOut == default || checkOut <= checkIn) checkOut = checkIn.AddDays(1);

            var reservation = new Reservation
            {
                ResidenceId = id,
                ReservationStartDate = checkIn,
                ReservationEndDate = checkOut
            };

            _context.Reservation.Add(reservation);
            _context.SaveChanges();

            var myReservations = _session.GetReservations();
            myReservations.Add(reservation);
            _session.SetReservations(myReservations);
            _cookies.SetReservationIds(myReservations);

            TempData["Message"] = "Success! Your residence has been reserved.";

            return RedirectToAction("Index", new
            {
                ActiveLocation = _session.GetActiveLocation(),
                ActiveCheckInDate = _session.GetActiveCheckInDate(),
                ActiveCheckOutDate = _session.GetActiveCheckOutDate(),
                ActiveNoOfGuests = _session.GetActiveNoOfGuests()
            });
        }




        [HttpPost]
        public IActionResult RemoveReservation(int id)
        {

            var reservation = _context.Reservation.Find(id);
            if (reservation != null)
            {
                _context.Reservation.Remove(reservation);
                _context.SaveChanges();
            }

            var reservations = _session.GetReservations();
            var reservationInSession = reservations.FirstOrDefault(r => r.ReservationId == id);
            if (reservationInSession != null)
            {
                reservations.Remove(reservationInSession);
                _session.SetReservations(reservations);
            }

            _cookies.RemoveReservationId(id);

            TempData["Message"] = "Reservation has been successfully cancelled.";
            return RedirectToAction("Reservations");
        }


        public IActionResult Details(int id)
        {
            var residence = _context.Residence
                .Include(r => r.Location)
                .FirstOrDefault(r => r.ResidenceId == id);

            var viewModel = new ResidenceViewModel
            {
                Residences = residence,
                ActiveLocation = _session.GetActiveLocation(),
                ActiveCheckInDate = _session.GetActiveCheckInDate(),
                ActiveCheckOutDate = _session.GetActiveCheckOutDate(),
                ActiveNoOfGuests = _session.GetActiveNoOfGuests()
            };

            return View(viewModel);
        }
        public IActionResult Support()
        {
            return Content("Area: Public, Controller: Home, Action: Support");
        }
        public IActionResult CancellationPolicy()
        {
            return Content("Area: Public, Controller: Home, Action: CancellationPolicy");
        }
        public IActionResult TermsConditions()
        {
            return Content("Area: Public, Controller: Home, Action: Terms and Conditions");
        }
        public IActionResult CookiesPolicy()
        {
            return Content("Area: Public, Controller: Home, Action: Cookie Policies");
        }
    }
}
