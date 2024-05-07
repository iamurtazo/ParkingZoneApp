using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingZoneApp.Models;
using ParkingZoneApp.Services;
using ParkingZoneApp.Services.ParkingSlotService;
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
}
