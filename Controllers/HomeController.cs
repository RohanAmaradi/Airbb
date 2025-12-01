using Airbb.Models;
using Airbb.Models.CookieExtensions;
using Airbb.Models.DataLayer;
using Airbb.Models.ExtensionMethods;
using Airbb.Models.ViewModels;
using Airbb.Models.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Airbb.Controllers
{
    public class HomeController : Controller
    {

        private AirbbSession _session => new AirbbSession(HttpContext.Session);
        private AirbbCookies _cookies => new AirbbCookies(Request.Cookies, Response.Cookies);

        private Repository<Location> locationRepo { get; set; }
        private Repository<Residence> residenceRepo { get; set; }
        private Repository<User> userRepo { get; set; }
        private Repository<Reservation> reservationRepo { get; set; }
        public HomeController(AirbbContext ctx)
        {
            locationRepo = new Repository<Location>(ctx);
            residenceRepo = new Repository<Residence>(ctx);
            userRepo = new Repository<User>(ctx);
            reservationRepo = new Repository<Reservation>(ctx);
        }

        public ViewResult Index(ResidenceViewModel model)
        {
            var filterKey = $"{model.ActiveLocation}-{model.ActiveCheckInDate}-{model.ActiveCheckOutDate}-{model.ActiveNoOfGuests}";
            var filterString = new Filters(filterKey);

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
                    var resOptions = new QueryOptions<Reservation> { Includes = "Residence,Residence.Location" };
                    var reservations = reservationRepo
                        .List(resOptions)
                        .Where(r => ids.Contains(r.GetType().GetProperty("ReservationId")!.GetValue(r)!.ToString()))
                        .ToList();

                    _session.SetReservations(reservations);
                }
            }

            var locOptions = new QueryOptions<Location> { OrderBy = l => l.Name };
            model.Location = locationRepo.List(locOptions).ToList();
            
            var resFilterOptions = new QueryOptions<Residence> { Includes = "Location" };
            var residences = residenceRepo.List(resFilterOptions).AsQueryable();

            DateTime.TryParse(model.ActiveCheckInDate, out DateTime checkInDate);
            DateTime.TryParse(model.ActiveCheckOutDate, out DateTime checkOutDate);

            if (filterString.HasCheckInDate && filterString.HasCheckOutDate)
            {
                var reservationDateOptions = new QueryOptions<Reservation>();
                var reservedResidenceIds = reservationRepo
                    .List(reservationDateOptions)
                    .Cast<Reservation>()
                    .Where(res =>
                        res.ReservationStartDate <= checkOutDate &&
                        res.ReservationEndDate >= checkInDate)
                    .Select(res => res.ResidenceId)
                    .Distinct()
                    .ToList();

                residences = residences.Where(r => !reservedResidenceIds.Contains(r.ResidenceId));
            }

            if (filterString.HasLocation)
            {
                residences = residences.Where(r => r.Location.LocationId.ToString() == model.ActiveLocation);
            }

            if (filterString.HasNoOfGuests && int.TryParse(model.ActiveNoOfGuests, out int guests))
            {
                residences = residences.Where(r => r.GuestNumber >= guests);
            }
            model.Residence = residences.OrderBy(r => r.Name).ToList();
            return View(model);
        }

        public IActionResult Reservations()
        {
            string[] reservationIds = _cookies.GetReservationIds();

            var options = new QueryOptions<Reservation>
            {
                Includes = "Residence,Residence.Location"
            };

            var reservations = reservationRepo
                .List(options)
                .Where(r => reservationIds.Contains(
                    r.GetType().GetProperty("ReservationId")!
                    .GetValue(r)!.ToString()
                ))
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
            reservationRepo.Insert(reservation);
            reservationRepo.Save();

            var myReservations = _session.GetReservations();
            myReservations.Add(reservation);
            _session.SetReservations(myReservations);

            _cookies.SetReservationIds(myReservations);
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
            var reservation = reservationRepo.Get(id);
            if (reservation != null)
            {
                reservationRepo.Delete(reservation);
                reservationRepo.Save();
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
            var options = new QueryOptions<Residence>
            {
                Includes = "Location",
                Where = r => r.ResidenceId == id
            };

            var residence = residenceRepo.Get(options);
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
