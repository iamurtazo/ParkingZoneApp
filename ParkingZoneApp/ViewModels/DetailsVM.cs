using ParkingZoneApp.Models;

namespace ParkingZoneApp.ViewModels
{
    public class DetailsVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime EstablishmentDate { get; set; }

        public DetailsVM(ParkingZone parkingZone)
        {
            Id = parkingZone.Id;
            Name = parkingZone.Name;
            Address = parkingZone.Address;
            EstablishmentDate = parkingZone.EstablishmentDate;
        }
    }
}
