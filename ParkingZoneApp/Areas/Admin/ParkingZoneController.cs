using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ParkingZoneApp.Data;
using ParkingZoneApp.Models;

namespace ParkingZoneApp.Areas.Admin
{
    [Area("Admin")]
    public class ParkingZoneController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParkingZoneController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/ParkingZone
        public async Task<IActionResult> Index()
        {
            return View(await _context.ParkingZones.ToListAsync());
        }

        // GET: Admin/ParkingZone/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkingZone = await _context.ParkingZones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkingZone == null)
            {
                return NotFound();
            }

            return View(parkingZone);
        }

        // GET: Admin/ParkingZone/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ParkingZone/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Address,EstablishmentDate")] ParkingZone parkingZone)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parkingZone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parkingZone);
        }

        // GET: Admin/ParkingZone/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkingZone = await _context.ParkingZones.FindAsync(id);
            if (parkingZone == null)
            {
                return NotFound();
            }
            return View(parkingZone);
        }

        // POST: Admin/ParkingZone/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address,EstablishmentDate")] ParkingZone parkingZone)
        {
            if (id != parkingZone.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parkingZone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkingZoneExists(parkingZone.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(parkingZone);
        }

        // GET: Admin/ParkingZone/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkingZone = await _context.ParkingZones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkingZone == null)
            {
                return NotFound();
            }

            return View(parkingZone);
        }

        // POST: Admin/ParkingZone/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parkingZone = await _context.ParkingZones.FindAsync(id);
            if (parkingZone != null)
            {
                _context.ParkingZones.Remove(parkingZone);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParkingZoneExists(int id)
        {
            return _context.ParkingZones.Any(e => e.Id == id);
        }
    }
}
