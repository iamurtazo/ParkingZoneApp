using Moq;
using ParkingZoneApp.Enums;
using ParkingZoneApp.Models;
using ParkingZoneApp.Repositories;
using ParkingZoneApp.Services.ParkingSlotService;
using System.Text.Json;

namespace ParkingSlots.Tests.Services;

public class ParkingSlotServiceTest
{
    private readonly Mock<IParkingSlotRepository> _parkingSlotRepository;
    private readonly IParkingSlotService _service;
    private readonly ParkingSlot _parkingSlot;
    private readonly int _id = 1;

    public ParkingSlotServiceTest()
    {
        _parkingSlotRepository = new Mock<IParkingSlotRepository>();
        _service = new ParkingSlotService(_parkingSlotRepository.Object);
        _parkingSlot = new ParkingSlot
        {
            Id = 1,
            Number = 1,
            IsAvailableForBooking = true,
            ParkingZoneId = 1,
            Category = SlotCategory.Premium
        };
    }

    #region Insert
    [Fact]
    public void GivenModel_WhenInsertIsCalled_ThenModelIsAdded()
    {
        //Arrange
        _parkingSlotRepository.Setup(x => x.Insert(_parkingSlot));

        //Act
        _service.Insert(_parkingSlot);

        //Assert
        _parkingSlotRepository.Verify(x => x.Insert(_parkingSlot), Times.Once);

    }
    #endregion

    #region Update
    [Fact]
    public void GivenModel_WhenUpdateIsCalled_ThenModelIsUpdated()
    {
        //Arrange
        _parkingSlotRepository.Setup(x => x.Update(_parkingSlot));

        //Act
        _service.Update(_parkingSlot);

        //Assert
        _parkingSlotRepository.Verify(x => x.Update(_parkingSlot), Times.Once);
    }
    #endregion

    #region Delete
    [Fact]
    public void GivenModel_WhenDeletIsCalled_ThenModelIsDeleted()
    {
        //Arrange
        _parkingSlotRepository.Setup(x => x.Delete(_parkingSlot));

        //Act
        _service.Delete(_parkingSlot);

        //Assert
        _parkingSlotRepository.Verify(x => x.Delete(_parkingSlot), Times.Once);

    }
    #endregion

    #region GetById
    [Fact]
    public void GivenId_WhenGetByIdIsCalled_ThenModelIsReturned()
    {
        //Arrange
        _parkingSlotRepository.Setup(x => x.GetById(_id)).Returns(_parkingSlot);

        //Act
        var result = _service.GetById(_id);

        //Assert
        Assert.IsType<ParkingSlot>(result);
        Assert.Equal(JsonSerializer.Serialize(_parkingSlot), JsonSerializer.Serialize(result));
        Assert.NotNull(result);
        _parkingSlotRepository.Verify(x => x.GetById(_id), Times.Once);
    }
    #endregion

    #region GetSlots
    [Fact]
    public void GivenParkingZoneId_WhenGetSlotsIsCalled_ThenReturnsParkingSlots()
    {
        //Arrange
        var expected = new List<ParkingSlot>() { _parkingSlot };
        _parkingSlotRepository.Setup(x => x.GetAll()).Returns(new List<ParkingSlot>() { _parkingSlot });

        //Act
        var result = _service.GetSlotsByZoneId(_id);

        //Assert
        Assert.IsAssignableFrom<IEnumerable<ParkingSlot>>(result);
        Assert.Equal(JsonSerializer.Serialize(result), JsonSerializer.Serialize(expected));
        Assert.NotNull(result);
        _parkingSlotRepository.Verify(x => x.GetAll(), Times.Once);
    }
    #endregion

    #region GetAll
    [Fact]
    public void GivenNothing_WhenGetAllIsCalled_ThenModelsAreReturned()
    {
        //Arrange
        var parkingSlots = new List<ParkingSlot>() { _parkingSlot };
        _parkingSlotRepository.Setup(x => x.GetAll()).Returns(parkingSlots);

        //Act
        var result = _service.GetAll();

        //Assert
        Assert.True(JsonSerializer.Serialize(result) == JsonSerializer.Serialize(parkingSlots));
        Assert.IsAssignableFrom<IEnumerable<ParkingSlot>>(parkingSlots);
        Assert.NotNull(result);
    }
    #endregion
}
