using ParkingZoneApp.Models;
using ParkingZoneApp.Repositories;

namespace ParkingZoneApp.Services.ParkingSlotService;

public class ParkingSlotService : Service<ParkingSlot>, IParkingSlotService
{
    private readonly IParkingSlotRepository _slotRepository;
    public ParkingSlotService(IParkingSlotRepository repository) : base(repository)
    {
        _slotRepository = repository;
    }

    public IEnumerable<ParkingSlot> GetSlotsByZoneId(int parkingZoneId)
    {
        return _slotRepository.GetAll()
                              .Where(slot => slot.ParkingZoneId == parkingZoneId);
    }

    public bool IsExistingParkingSlot(int parkingZoneId, int slotNumber)
    {
        return _slotRepository.GetAll()
                              .Any(slot => slot.ParkingZoneId == parkingZoneId && slot.Number == slotNumber);
    }
}
