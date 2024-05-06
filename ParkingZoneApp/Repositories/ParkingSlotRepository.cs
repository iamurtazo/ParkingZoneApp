using ParkingZoneApp.Data;
using ParkingZoneApp.Models;

namespace ParkingZoneApp.Repositories;

public class ParkingSlotRepository: Repository<ParkingSlot>, IParkingSlotRepository
{
	public ParkingSlotRepository(ApplicationDbContext context) : base(context)
	{

	}
}
