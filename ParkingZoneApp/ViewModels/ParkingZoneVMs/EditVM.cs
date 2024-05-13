using ParkingZoneApp.Models;
using System.ComponentModel.DataAnnotations;

namespace ParkingZoneApp.ViewModels
{
    public class EditVM
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
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
