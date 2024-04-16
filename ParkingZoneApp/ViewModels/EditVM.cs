using ParkingZoneApp.Models;

namespace ParkingZoneApp.ViewModels
{
    public class EditVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime EstablishmentDate { get; set; }

        public EditVM()
        {
            
        }

        public EditVM(ParkingZone parkingZone)
        {
            Id = parkingZone.Id;
            Name = parkingZone.Name;
            Address = parkingZone.Address;
            EstablishmentDate = parkingZone.EstablishmentDate;
        }

        public ParkingZone MapToModel(EditVM editVM)
        {
            return new ParkingZone()
            {
                Id = editVM.Id,
                Name = editVM.Name,
                Address = editVM.Address,
                EstablishmentDate = editVM.EstablishmentDate
            };
        }
    }
}
