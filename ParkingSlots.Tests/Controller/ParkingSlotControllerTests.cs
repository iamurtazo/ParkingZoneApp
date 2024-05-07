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
    private readonly int _id = 1;

    public ParkingSlotControllerTests()
    {
        _slotService = new Mock<IParkingSlotService>();
        _zoneService = new Mock<IParkingZoneService>();
        _controller = new ParkingSlotController(_slotService.Object, _zoneService.Object);
        _parkingZone = new () { Id = _id, Name = "Zone 1" };
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

        _slotService.Setup(x => x.GetSlotsByZoneId(_id)).Returns(_parkingSlot);
        _zoneService.Setup(x => x.GetById(_id)).Returns(_parkingZone);

        //Act
        var index = _controller.Index(_id);
        var model = ((ViewResult)index).Model;

        //Assert
        Assert.IsType<ViewResult>(index);
        Assert.IsAssignableFrom<IEnumerable<ListOfParkingSlotsVM>>(model);
        Assert.Equal(JsonSerializer.Serialize(model), JsonSerializer.Serialize(expectedVMs));
        Assert.NotNull(index);
        Assert.NotNull(model);
        _slotService.Verify(_parkingSlot => _parkingSlot.GetSlotsByZoneId(_id), Times.Once);
    }
    #endregion

    #region Create
    [Fact]
    public void GivenParkingZoneId_WhenCreateIsCalled_ThenReturnsCreateViewModel()
    {
        //Arrange
        var createVM = new CreateViewModel() { ParkingZoneId = _id };

        //Act
        var create = _controller.Create(_id);
        var model = ((ViewResult)create).Model;

        //Assert
        Assert.IsType<ViewResult>(create);
        Assert.IsAssignableFrom<CreateViewModel>(model);
        Assert.Equal(JsonSerializer.Serialize(model), JsonSerializer.Serialize(createVM));
        Assert.NotNull(create);
    }

    [Fact]
    public void GivenCreateModel_WhenCreateIsCalled_ThenSlotExistsAndReturnsViewResult()
    {
        //Arrange
        var createVM = new CreateViewModel() { ParkingZoneId = _id, Number = 2 };
        _controller.ModelState.AddModelError("Number", "Slot number already exists in this zone");
        _slotService.Setup(p => p.IsExistingParkingSlot(createVM.ParkingZoneId, createVM.Number))
                    .Returns(true);

        //Act
        var create = _controller.Create(new CreateViewModel());

        //Assert
        Assert.IsType<ViewResult>(create);
        Assert.False(_controller.ModelState.IsValid);
        Assert.NotNull(create);
    }
    [Fact]
    public void GivenCreateModel_WhenCreateIsCalled_ThenSlotNumberIsNegativeAndReturnsViewResult()
    {
        //Arrange
        _controller.ModelState.AddModelError("Number", "Number can not be negative");
        CreateViewModel createModel = new() { ParkingZoneId = 1, Number = -5 };
        _slotService.Setup(s => s.IsExistingParkingSlot(createModel.ParkingZoneId, createModel.Number))
                    .Returns(true);

        //Act
        var create = _controller.Create(new CreateViewModel());

        //Assert
        Assert.IsType<ViewResult>(create);
        Assert.IsType<CreateViewModel>(createModel);
        Assert.True(_controller.ModelState.IsValid);
        Assert.NotNull(create);
    }

    [Fact]
    public void GivenCreateModel_WhenCreateIsCalled_ThenModelStateIsFalseAndReturnsViewResult()
    {
        //Arrange
        CreateViewModel createVm = new();
        _controller.ModelState.AddModelError("Category", "Category is required");

        //Act
        var create = _controller.Create(createVm);

        //Assert
        Assert.IsType<ViewResult>(create);
        Assert.False(_controller.ModelState.IsValid);

    }
    [Fact]
    public void GivenCreateModel_WhenCreateIsCalled_ThenModelStateIsTrueAndReturnsRedirectToActionResult()
    {
        //Arrange
        var createVM = new CreateViewModel() { ParkingZoneId = 3, Number = 2 };
        _slotService.Setup(p => p.IsExistingParkingSlot(createVM.ParkingZoneId, createVM.Number))
                    .Returns(false);
        _slotService.Setup(p => p.Insert(It.IsAny<ParkingSlot>()));

        //Act
        var create = _controller.Create(createVM);
        var model = create as RedirectToActionResult;

        //Assert
        Assert.Equal("Index", model.ActionName);
        Assert.IsType<RedirectToActionResult>(create);
        Assert.True(_controller.ModelState.IsValid);
        Assert.NotNull(create);
        _slotService.Verify(p => p.Insert(It.IsAny<ParkingSlot>()), Times.Once);
        _slotService.Verify(p => p.IsExistingParkingSlot(createVM.ParkingZoneId, createVM.Number), Times.Once);
    }
    #endregion
}
