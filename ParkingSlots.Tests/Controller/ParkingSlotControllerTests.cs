using Microsoft.AspNetCore.Mvc;
using Moq;
using ParkingZoneApp.Areas.Admin.Controllers;
using ParkingZoneApp.Enums;
using ParkingZoneApp.Models;
using ParkingZoneApp.Services;
using ParkingZoneApp.Services.ParkingSlotService;
using ParkingZoneApp.ViewModels.ParkingSlotViewModels;
using System.Text.Json;

namespace ParkingSlots.Tests.Controller;

public class ParkingSlotControllerTests
{
    private readonly Mock<IParkingSlotService> _slotService;
    private readonly Mock<IParkingZoneService> _zoneService;
    private readonly ParkingSlotController _controller;
    private readonly ParkingZone _parkingZone;
    private readonly List<ParkingSlot> _parkingSlot;

    private readonly int _Id = 1;

    public ParkingSlotControllerTests()
    {
        _slotService = new Mock<IParkingSlotService>();
        _zoneService = new Mock<IParkingZoneService>();
        _controller = new ParkingSlotController(_slotService.Object, _zoneService.Object);
        _parkingZone = new () { Id = _Id, Name = "Zone 1" };
        _parkingSlot = new List<ParkingSlot>
        {
            new ()
            {
                Id = 1,
                Number = 2,
                IsAvailableForBooking = false,
                ParkingZoneId = 1,
                Category = 0,

            },
            new ()
            {
                Id = 2,
                Number = 3,
                IsAvailableForBooking = true,
                ParkingZoneId = 1,
                Category = SlotCategory.Premium
            }
        };
        
    }

    #region Index
    [Fact]
    public void GivenParkingZoneId_WhenIndexIsCalled_ThenReturnsParkingSlotsVM()
    {
        // Arrange
        var expectedVMs = new List<ListOfParkingSlotsVM>();
        expectedVMs.AddRange(ListOfParkingSlotsVM.MapToModel(_parkingSlot));

        _slotService
            .Setup(x => x.GetSlots(_Id))
            .Returns(_parkingSlot);

        //Act
       var index = _controller.Index(_Id);
        var model = ((ViewResult)index).Model;

        //Assert
        Assert.IsType<ViewResult>(index);
        Assert.IsAssignableFrom<IEnumerable<ListOfParkingSlotsVM>>(model);
        Assert.Equal(JsonSerializer.Serialize(model), JsonSerializer.Serialize(expectedVMs));
        Assert.NotNull(index);
        Assert.NotNull(model);
        _slotService.Verify(_parkingSlot => _parkingSlot.GetSlots(_Id), Times.Once);
    }
    #endregion
}
