using ParkingZoneApp.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ParkingZoneApp.ViewModels
{
    public class ListOfItemsVM
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [DisplayName("Establishment Date")]
        public DateTime EstablishmentDate { get; set; }
        public ListOfItemsVM()
        {
            
        }

        public ListOfItemsVM(ParkingZone parkingZone)
        {
            Id = parkingZone.Id;
            Name = parkingZone.Name;
            Address = parkingZone.Address;
            EstablishmentDate = parkingZone.EstablishmentDate;
        }
        public static IEnumerable<ListOfItemsVM> MapToModel(IEnumerable<ParkingZone> parkingZones)
        {
            return parkingZones.Select(parkingZone => new ListOfItemsVM(parkingZone));
        }
    }
}
