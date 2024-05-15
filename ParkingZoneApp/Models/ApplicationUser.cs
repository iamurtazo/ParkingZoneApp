using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ParkingZoneApp.Models;

public class ApplicationUser : IdentityUser
{
    [Required]
    [DisplayName("Name")]
    public string Name { get; set; }
}
