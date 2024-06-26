﻿using ParkingZoneApp.Models;
using ParkingZoneApp.Repositories;

namespace ParkingZoneApp.Services.ParkingZoneService;

public class ParkingZoneService : Service<ParkingZone>, IParkingZoneService
{
    public ParkingZoneService(IParkingZoneRepository repository) : base(repository)
    {
    }
}
