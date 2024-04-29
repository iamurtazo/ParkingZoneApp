using ParkingZoneApp.Data;
using ParkingZoneApp.Models;
using ParkingZoneApp.Repositories.ParkingZoneRepository.ParkingZoneRepositories;

namespace ParkingZoneApp.Repositories.ParkingZoneRepositories
{
    public class ParkingZoneRepository : Repository<ParkingZone>, IParkingZoneRepository
    {
        public ParkingZoneRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
