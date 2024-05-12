using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingZoneApp.Models;
using ParkingZoneApp.Services;
using ParkingZoneApp.Services.ParkingSlotService;
using ParkingZoneApp.ViewModels;
using ParkingZoneApp.ViewModels.ParkingSlotViewModels;

namespace ParkingZoneApp.Areas.Admin.Controllers;

[Authorize]
[Area("Admin")]
public class ParkingSlotController : Controller
{
    private readonly IParkingSlotService _slotService;
    private readonly IParkingZoneService _zoneService;

    public ParkingSlotController(IParkingSlotService _slot, IParkingZoneService _zone)
    {
        _slotService = _slot;
        _zoneService = _zone;
    }

    #region Index
    // GET: Admin/ParkingSlot
    public IActionResult Index(int parkingZoneId)
    {
        var parkingSlotVMs = _slotService
                                .GetSlotsByZoneId(parkingZoneId)
                                .Select(slot => new ListOfParkingSlotsVM(slot));

        ViewData["parkingZoneId"] = parkingZoneId;
        ViewData["parkingZoneName"] = _zoneService.GetById(parkingZoneId).Name;

        return View(parkingSlotVMs);
    }
    #endregion

    #region Create
    // GET: Admin/ParkingSlot/Create
    public IActionResult Create(int parkingZoneId)
    {
        return View(new CreateViewModel() { ParkingZoneId = parkingZoneId});
    }

    // POST: Admin/ParkingSlot/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CreateViewModel createModel)
    {
        if(_slotService.IsExistingParkingSlot(createModel.ParkingZoneId, createModel.Number))
        {
            ModelState.AddModelError("Number", "Slot number already exists in this zone");
        }

        if (ModelState.IsValid)
        {
            ParkingSlot parkingSlot = createModel.MapToModel();
            _slotService.Insert(parkingSlot);
            return RedirectToAction(nameof(Index), new {ParkingZoneId = createModel.ParkingZoneId} );
        }
        return View(createModel);
    }
    #endregion

    #region Edit
    // GET: Admin/ParkingSlot/Edit/5
    public IActionResult Edit(int? id)
    {
        var slot = _slotService.GetById(id);
        if (slot == null)
        {
            return NotFound();
        }
        var editModel = new EditViewModel(slot);
        return View(editModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, EditViewModel editModel)
    {
        if (id != editModel.Id)
        {
            return NotFound();
        }

        var parkingSlot = _slotService.GetById(id);
        var isExistingParkingSlot = _slotService.IsExistingParkingSlot(editModel.ParkingZoneId, editModel.Number);

        if (isExistingParkingSlot && editModel.Number != parkingSlot.Number)
        {
            ModelState.AddModelError("Number", "Slot with this number exists!");
        }

        if (ModelState.IsValid)
        {
             parkingSlot = editModel.MapToModel(parkingSlot);
            _slotService.Update(parkingSlot);
            return RedirectToAction(nameof(Index), new { parkingSlot.ParkingZoneId });
        }
        return View(editModel);
    }
    #endregion
}
