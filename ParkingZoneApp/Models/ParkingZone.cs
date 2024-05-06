using System.ComponentModel.DataAnnotations;

namespace ParkingZoneApp.Models;

public class ParkingZone
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public DateTime EstablishmentDate { get; set; }

}
