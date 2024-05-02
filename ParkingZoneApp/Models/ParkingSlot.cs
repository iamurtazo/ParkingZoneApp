using ParkingZoneApp.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ParkingZoneApp.Models;

public class ParkingSlot
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    public int Number { get; set; }
    [Required]
    public bool IsAvailableForBooking { get; set; }
    [Required]
    public int ParkingZoneId { get; set; }
    [Required]
    public ParkingZone ParkingZone { get; set; }
    [Required]
    public SlotCategory Category { get; set; }
}
