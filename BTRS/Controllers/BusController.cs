using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BTRS.Data;
using BTRS.Models;

namespace BTRS.Controllers
{
    public class BusController : Controller
    {
        private readonly SystemDbContext _context;

        public BusController(SystemDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Trip = _context.trip.ToList();
            return View(await _context.Bus.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.Trip = _context.trip.ToList();
            if (id == null)
            {
                return NotFound();
            }

            var bus = await _context.Bus
                .FirstOrDefaultAsync(m => m.ID == id);
            if (bus == null)
            {
                return NotFound();
            }

            return View(bus);
        }

        public IActionResult Create()
        {
            ViewBag.Trip = _context.trip.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection form)
        {
            int adminID = (int)HttpContext.Session.GetInt32("adminid");

            string captain_name = form["captain_name"];
            int numOfSeats = int.Parse(form["numOfSeats"]);
            int tripID = int.Parse(form["TripID"]);

            Bus bus = new Bus();
            bus.administrators = _context.administrators.Find(adminID);
            bus.captain_name = captain_name;
            bus.numOfSeats = numOfSeats;
            bus.trip = _context.trip.Find(tripID);

            _context.Bus.Add(bus);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Trip = _context.trip.ToList();
            if (id == null)
            {
                return NotFound();
            }

            var bus = await _context.Bus.FindAsync(id);
            if (bus == null)
            {
                return NotFound();
            }
            return View(bus);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormCollection form)
        {
            int adminID = (int)HttpContext.Session.GetInt32("adminid");

            string captain_name = form["captain_name"];
            int numOfSeats = int.Parse(form["numOfSeats"]);
            int tripID = int.Parse(form["TripID"]);

            //bus-id
            id = int.Parse(form["ID"]);

            Bus bus = _context.Bus.Find(id);

            bus.administrators = _context.administrators.Find(adminID);
            bus.captain_name = captain_name;
            bus.numOfSeats = numOfSeats;
            bus.trip = _context.trip.Find(tripID);

            _context.Bus.Update(bus);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            Bus bus = _context.Bus.Find(id);
            _context.Bus.Remove(bus);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
