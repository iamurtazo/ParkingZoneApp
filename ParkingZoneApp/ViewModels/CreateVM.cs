using ParkingZoneApp.Models;

namespace ParkingZoneApp.ViewModels
{
    public class CreateVM
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public ParkingZone MapToModel()
        {
            return new ParkingZone()
            {
                Name = Name,
                Address = Address,
                EstablishmentDate = DateTime.Now
            };
        }
    }
}
