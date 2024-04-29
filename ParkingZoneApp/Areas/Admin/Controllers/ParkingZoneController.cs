using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingZoneApp.Services;
using ParkingZoneApp.ViewModels;

namespace ParkingZoneApp.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class ParkingZoneController : Controller
    {
        private readonly IParkingZoneService _service;

        public ParkingZoneController(IParkingZoneService service)
        {
            _service = service;
        }

        // GET: Admin/ParkingZone
        public IActionResult Index()
        {
            var parkingZones = _service.GetAll();
            var viewModels = ListOfItemsVM.MapToModel(parkingZones);
            return View(viewModels);
        }

        // GET: Admin/ParkingZone/Details/5
        public IActionResult Details(int? id)
        {
            var parkingZone = _service.GetById(id);
            if (parkingZone == null)
            {
                return NotFound();
            }
            var detailsModel = new DetailsVM(parkingZone);
            return View(detailsModel);
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
        public IActionResult Create(CreateVM createModel)
        {
            if (ModelState.IsValid)
            {
                var parkingZone = createModel.MapToModel();
                _service.Insert(parkingZone);
                return RedirectToAction(nameof(Index));
            }
            return View(createModel);
        }

        // GET: Admin/ParkingZone/Edit/5
        public IActionResult Edit(int? id)
        {
            var parkingZone = _service.GetById(id);
            if (parkingZone is null)
            {
                return NotFound();
            }
            var editModel = new EditVM(parkingZone);
            return View(editModel);
        }

        // POST: Admin/ParkingZone/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EditVM editVM)
        {
            if (id != editVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var parkingZone = editVM.MapToModel(editVM);
                    _service.Update(parkingZone);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkingZoneExists(editVM.Id))
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
            return View();
        }

        // GET: Admin/ParkingZone/Delete/5
        public IActionResult Delete(int? id)
        {   
            var parkingZone = _service.GetById(id);
            if (parkingZone is null)
            {
                return NotFound();
            }

            return View(parkingZone);
        }

        // POST: Admin/ParkingZone/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var parkingZone = _service.GetById(id);
            if (parkingZone != null)
            {
                _service.Delete(parkingZone);
                return RedirectToAction(nameof(Index));

            }
            return NotFound();
        }

        private bool ParkingZoneExists(int id)
        {
            return _service.GetById(id) != null;
        }
    }
}
