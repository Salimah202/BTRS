using BTRS.Data;
using BTRS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BTRS.Controllers
{
    public class UserController : Controller

    {
        private SystemDbContext _context;
        public UserController(SystemDbContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
       
        
        public IActionResult signup()
        {
            return View();
        }
        [HttpPost]
        public IActionResult signup(Passengers passenger)
        {
            bool notempty = checkEmpty(passenger);
            bool duplicat = checkNoDuplicate(passenger);

            if (notempty)
            {
                if (duplicat)
                {
                    HttpContext.Session.SetInt32("passengerid", passenger.ID);
                    _context.passengers.Add(passenger);
                    _context.SaveChanges();

                    TempData["Msg"] = "the data was saved";
                    return RedirectToAction("TripList");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                TempData["Msg"] = "Please fill all input ";
                return View();
            }

        }


        public bool checkNoDuplicate(Passengers passenger)
        {

            Passengers passenger_username = _context.passengers.Where(u => u.username.Equals(passenger.username)).FirstOrDefault();
            Passengers passenger_phone = _context.passengers.Where(u => u.phone_number.Equals(passenger.phone_number)).FirstOrDefault();
            Passengers passenger_email = _context.passengers.Where(u => u.email_address.Equals(passenger.email_address)).FirstOrDefault();
            if (passenger_username != null)
            {
                TempData["Msg"] = "Please Change the username";
                return false;
            }
            else if (passenger_phone != null)
            {
                TempData["Msg"] = "Please Change phone number";
                return false;
            }
            else if(passenger_email != null)
            {
                TempData["Msg"] = "Please Change your email";
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool checkEmpty(Passengers passenger)
        {
            if (String.IsNullOrEmpty(passenger.username)) return false;
            else if (String.IsNullOrEmpty(passenger.password)) return false;
            else if (String.IsNullOrEmpty(passenger.name)) return false;
            else if (String.IsNullOrEmpty(passenger.phone_number)) return false;
            else if (String.IsNullOrEmpty(passenger.email_address)) return false;
            else return true;
        }

        [HttpGet]
        public IActionResult login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult login(Login userlogin )
        {
            if (ModelState.IsValid)
            {
                string username = userlogin.username;
                string password = userlogin.password;

                Passengers passenger = _context.passengers.Where(u => u.username.Equals(username) &&
                u.password.Equals(password)).FirstOrDefault();

                Administrators admin = _context.administrators.Where(u => u.userName.Equals(username)
                && u.password.Equals(password)).FirstOrDefault();

                if (passenger != null)
                {
                    HttpContext.Session.SetInt32("passengerid", passenger.ID);
                    return RedirectToAction("TripList");
                }
                else if (admin != null)
                {
                    HttpContext.Session.SetInt32("adminid",admin.ID);
                    return RedirectToAction("tripOrBus");
                }
                else
                {
                    TempData["msg"] = "the user not found";
                    return View();
                }
            }
            else
            {
                TempData["msg"] = "please insert data";
                return View();
            }
        }
       
        public IActionResult TripList()
        {
            int passengerID = (int)HttpContext.Session.GetInt32("passengerid");
            List<int>lst_picked_trip=_context.passenger_Trip.Where(t=>t.passenger.ID.Equals(passengerID)).
                Select(u=>u.trip.ID).ToList();
            List<Trip>lst_unpicked_trip=_context.trip.Where(u=>lst_picked_trip.Contains(u.ID)==false).ToList();
            return View(lst_unpicked_trip);
        }
        public IActionResult AddTrip(int id)
        {
            int tripID = id;
            int passengerID=(int)HttpContext.Session.GetInt32("passengerid");
            Passenger_Trip passenger_trip=new Passenger_Trip();
            passenger_trip.passenger = _context.passengers.Find(passengerID);
            passenger_trip.trip = _context.trip.Find(tripID);
            _context.passenger_Trip.Add(passenger_trip);
            _context.SaveChanges();
            return RedirectToAction("pickedtrip_List");
        }
        public IActionResult pickedtrip_List()
        {
            int passengerID = (int)HttpContext.Session.GetInt32("passengerid");
            List<int> lst_trip = _context.passenger_Trip.Where(u => u.passenger.ID== passengerID).Select(s => s.trip.ID).ToList();
            List<Trip>lst=_context.trip.Where(t=>lst_trip.Contains(t.ID)).ToList();
            return View(lst);
        }
        public IActionResult delete_picked_trip(int tripID)
        {
            int passengerID = (int)HttpContext.Session.GetInt32("passengerid");
            Passenger_Trip passenger_trip = _context.passenger_Trip.Where(t => t.passenger.ID == passengerID && t.trip.ID == tripID).FirstOrDefault();
            _context.passenger_Trip.Remove(passenger_trip);
            _context.SaveChanges();
            return RedirectToAction("pickedtrip_List");
        }
        public IActionResult tripOrBus()
        {
            return View();
        }
    }
}

