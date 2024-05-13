using ParkingZoneApp.Enums;
using System.ComponentModel;
using ParkingZoneApp.Models;
using System.ComponentModel.DataAnnotations;

namespace ParkingZoneApp.ViewModels.ParkingSlotVMs;

public class DetailsVM
{
    [Required]
    public int Id { get; set; }
    [Required]
    public int Number { get; set; }
    [Required]
    [DisplayName("Availability For Booking")]
    public bool IsAvailableForBooking { get; set; }
    [Required]
    public int ParkingZoneId { get; set; }
    [Required]
    public SlotCategory Category { get; set; }

    public DetailsVM() { }

    public DetailsVM(ParkingSlot slot)
    {
        Id = slot.Id;
        Number = slot.Number;
        IsAvailableForBooking = slot.IsAvailableForBooking;
        ParkingZoneId = slot.ParkingZoneId;
        Category = slot.Category;
    }
}
