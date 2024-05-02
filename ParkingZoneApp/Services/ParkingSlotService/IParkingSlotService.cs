using ParkingZoneApp.Models;

namespace ParkingZoneApp.Services.ParkingSlotService;

public interface IParkingSlotService : IService<ParkingSlot>
{
    IEnumerable<ParkingSlot> GetSlots(int parkingZoneId);
}
