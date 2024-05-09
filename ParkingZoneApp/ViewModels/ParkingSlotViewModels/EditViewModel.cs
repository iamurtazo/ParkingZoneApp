using ParkingZoneApp.Enums;
using ParkingZoneApp.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ParkingZoneApp.ViewModels.ParkingSlotViewModels;

public class EditViewModel
{
    [Required]
    public int Id { get; set; }
    [Required]
    public int Number { get; set; }
    [Required]
    [DisplayName("Available For Booking")]
    public bool IsAvailableForBooking { get; set; }
    [Required]
    public int ParkingZoneId { get; set; }
    [Required]
    public SlotCategory Category { get; set; }
    
    public EditViewModel(ParkingSlot slot)
    {
        Id = slot.Id;
        Number = slot.Number;
        IsAvailableForBooking = slot.IsAvailableForBooking;
        ParkingZoneId = slot.ParkingZoneId;
        Category = slot.Category;
    }
    public EditViewModel()
    {
        
    }

    public ParkingSlot MapToModel(EditViewModel editModel)
    {
        return new()
        {
            Id = editModel.Id,
            Number = editModel.Number,
            IsAvailableForBooking = editModel.IsAvailableForBooking,
            ParkingZoneId = editModel.ParkingZoneId,
            Category = editModel.Category
        };
    }
}
