using ParkingZoneApp.Enums;
using ParkingZoneApp.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ParkingZoneApp.ViewModels.ParkingSlotViewModels;

public class DetailsViewModel
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

    public DetailsViewModel() { }

    public DetailsViewModel(ParkingSlot slot)
    {
        Id = slot.Id;
        Number = slot.Number;
        IsAvailableForBooking = slot.IsAvailableForBooking;
        ParkingZoneId = slot.ParkingZoneId;
        Category = slot.Category;
    }
}
