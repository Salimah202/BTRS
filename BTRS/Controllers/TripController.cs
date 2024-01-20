using BTRS.Data;
using BTRS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BTRS.Controllers
{
    public class TripController : Controller
    {
        private SystemDbContext _context;
        public TripController (SystemDbContext context)
        {
            this._context = context;
        }
        // GET: TripController
        public ActionResult Index()
        {
            return View(_context.trip.ToList());
        }

        // GET: TripController/Details/5
        public ActionResult Details(int id)
        {
            Trip trip = _context.trip.Find(id);
            return View(trip);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Trip trip)
        {
            try
            {
                int adminID= (int)HttpContext.Session.GetInt32("adminid");
                Administrators admin=_context.administrators.Where(a=>a.ID== adminID).FirstOrDefault();
                trip.administrators = admin;
                _context.trip.Add(trip);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            Trip trip = _context.trip.Find(id);
            return View(trip);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Trip trip)
        {
            try
            {
                _context.trip.Update(trip);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            Trip trip= _context.trip.Find(id);
            _context.trip.Remove(trip);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
