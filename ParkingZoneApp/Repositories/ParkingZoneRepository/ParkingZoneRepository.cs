using ParkingZoneApp.Data;
using ParkingZoneApp.Models;
using ParkingZoneApp.Repositories.ParkingZoneRepository.ParkingZoneRepository;

namespace ParkingZoneApp.Repositories.ParkingZoneRepositoryy
{
    public class ParkingZoneRepository : Repository<ParkingZone>, IParkingZoneRepository
    {
        public ParkingZoneRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
