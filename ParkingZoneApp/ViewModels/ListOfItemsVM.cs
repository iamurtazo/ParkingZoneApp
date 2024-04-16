using ParkingZoneApp.Models;
using System.Collections.Generic;

namespace ParkingZoneApp.ViewModels
{
    public class ListOfItemsVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime EstablishmentDate { get; set; }

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
