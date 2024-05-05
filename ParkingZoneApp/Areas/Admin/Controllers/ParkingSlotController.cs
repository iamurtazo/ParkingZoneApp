using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        return View(parkingSlotVMs);
    }
    #endregion
}
