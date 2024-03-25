using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ParkingZone.Areas.Identity.Data;
using ParkingZone.Models;
using System.Diagnostics;

namespace ParkingZone.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ParkingZoneUser> _parkingZoneUser;

        public HomeController(ILogger<HomeController> logger, UserManager<ParkingZoneUser> parkingZoneUser)
        {
            _logger = logger;
            _parkingZoneUser = parkingZoneUser;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
