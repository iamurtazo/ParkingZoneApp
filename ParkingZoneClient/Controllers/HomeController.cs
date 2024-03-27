using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ParkingZoneClient.Data;
using ParkingZoneClient.Models;
using System.Diagnostics;

namespace ParkingZoneClient.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<Data.IdentityUser> _parkingZoneUser;

        public HomeController(ILogger<HomeController> logger, UserManager<Data.IdentityUser> parkingZoneUser)
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
