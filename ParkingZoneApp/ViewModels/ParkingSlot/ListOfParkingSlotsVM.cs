using ParkingZoneApp.Enums;
using ParkingZoneApp.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ParkingZoneApp.ViewModels.ParkingSlotViewModels
{
    public class ListOfParkingSlotsVM
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int Number { get; set; }
        [Required]
        [DisplayName("Is Available For Booking")]
        public bool IsAvailableForBooking { get; set; }
        [Required]
        public int ParkingZoneId { get; set; }
        [Required]
        public SlotCategory Category { get; set; }
        public ListOfParkingSlotsVM()
        {
            
        }

        public ListOfParkingSlotsVM(ParkingSlot slot)
        {
            Id = slot.Id;
            Number = slot.Number;
            IsAvailableForBooking = slot.IsAvailableForBooking;
            ParkingZoneId = slot.ParkingZoneId;
            Category = slot.Category;
        }

        public static IEnumerable<ListOfParkingSlotsVM> MapToModel(IEnumerable<ParkingSlot> slots)
        {
            return slots.Select(slot => new ListOfParkingSlotsVM(slot));
        }
    }
}
