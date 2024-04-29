using ParkingZoneApp.Data;
using ParkingZoneApp.Models;
using ParkingZoneApp.Repositories;

namespace ParkingZoneApp.Repository
{
    public class ParkingZoneRepository : Repository<ParkingZone>, IParkingZoneRepository
    {
        public ParkingZoneRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
