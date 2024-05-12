using Microsoft.AspNetCore.Mvc;
using Moq;
using ParkingZoneApp.Areas.Admin.Controllers;
using ParkingZoneApp.Models;
using ParkingZoneApp.Services;
using ParkingZoneApp.ViewModels;
using System.Text.Json;

namespace ParkingZoneApp.Tests.Controller
{
    public class ParkingZoneControllerTests
    {
        private readonly Mock<IParkingZoneService> _service;
        private readonly ParkingZoneController _parkingZoneController;
        private readonly List<ParkingZone> _parkingZones;

        #region Constructor
        public ParkingZoneControllerTests()
        {
            _service = new Mock<IParkingZoneService>();
            _parkingZoneController = new ParkingZoneController(_service.Object);
            _parkingZones = new()
            {
                new ()
                {
                    Id = 1,
                    Name = "Kalimanjaro",
                    Address = "Tub",
                    EstablishmentDate = DateTime.Now
                },
                new ()
                {
                    Id = 2,
                    Name = "Kabritklin",
                    Address = "Bermount palaroid",
                    EstablishmentDate = DateTime.Now
                }
            };
        }
        #endregion

        #region Index
        [Fact]
        public void GivenNothing_WhenIndexIsCalled_ThenReturnsListOfViewModels()
        {
            //Arrange
            var expectedParkingZones = new List<ParkingZone>();
            expectedParkingZones.AddRange(_parkingZones);

            var expectedViewModels = new List<ListOfItemsVM>();
            expectedViewModels.AddRange(ListOfItemsVM.MapToModel(expectedParkingZones));

            _service
                .Setup(_service => _service.GetAll())
                .Returns(expectedParkingZones);

            //Act
            var index = _parkingZoneController.Index();
            var model = (index as ViewResult).Model;

            //Assert 
            Assert.IsType<ViewResult>(index);
            Assert.IsAssignableFrom<IEnumerable<ListOfItemsVM>>(model);
            Assert.Equal(JsonSerializer.Serialize(expectedViewModels), JsonSerializer.Serialize(model));
            Assert.NotNull(index);
            Assert.NotNull(model);
            _service.Verify(_service => _service.GetAll(), Times.Once);
        }
        #endregion

        #region Details
        [Fact]
        public void GivenId_WhenDetailsIsCalled_ThenReturnsNotFound()
        {
            //Arrange
            _service.Setup(_service => _service.GetById(_parkingZones[0].Id));

            //Act
            var details = _parkingZoneController.Details(_parkingZones[0].Id);

            //Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(details);
            Assert.NotNull(details);
            Assert.Equal(404, notFoundResult.StatusCode);
            _service.Verify(_service => _service.GetById(_parkingZones[0].Id), Times.Once);
        }

        [Fact]
        public void GivenId_WhenDetailsIsCalled_ThenReturnsDetailsViewModel()
        {
            //Arrange
            _service.Setup(service => service.GetById(_parkingZones[0].Id)).Returns(_parkingZones[0]);
            var expectedViewModel = new DetailsVM(_parkingZones[0]);

            //Act
            var details = _parkingZoneController.Details(_parkingZones[0].Id);
            var model = (details as ViewResult).Model;

            //Assert
            Assert.IsType<ViewResult>(details);
            Assert.IsType<DetailsVM>(model);
            Assert.Equal(JsonSerializer.Serialize(expectedViewModel), JsonSerializer.Serialize(model));
            Assert.NotNull(details);
            Assert.NotNull(model);
            _service.Verify(_service => _service.GetById(_parkingZones[0].Id), Times.Once);
        }
        #endregion

        #region Create
        [Fact]
        public void GivenNothing_WhenCreateIsCalled_ThenReturnsCreateView()
        {
            //Arrange

            //Act
            var create = _parkingZoneController.Create();

            //Assert
            Assert.IsType<ViewResult>(create);
        }

        [Fact]
        public void GivenModel_WhenCreateIsCalled_ThenModelStateIsFalseAndReturnsViewResult()
        {
            //Arrange
            CreateVM createModel = new();
            _parkingZoneController.ModelState.AddModelError("Name", "Name is required");

            //Act
            var create = _parkingZoneController.Create(createModel);
            var model = (create as ViewResult).Model;

            //Assert
            Assert.IsType<ViewResult>(create);
            Assert.IsType<CreateVM>(model);
            Assert.NotNull(create);
            Assert.False(_parkingZoneController.ModelState.IsValid);
        }
        [Fact]
        public void GivenModel_WhenCreateIsCalled_ThenModelStateIsTrueAndReturnsRedirectToAction()
        {
            //Arrange
            CreateVM createViewModel = new();
            _service.Setup(service => service.Insert(It.IsAny<ParkingZone>()));

            //Act
            var create = _parkingZoneController.Create(createViewModel);
            var model = create as RedirectToActionResult;

            //Assert
            Assert.IsType<RedirectToActionResult>(create);
            Assert.Equal("Index", model.ActionName);
            Assert.True(_parkingZoneController.ModelState.IsValid);
            _service.Verify(service => service.Insert(It.IsAny<ParkingZone>()), Times.Once);
        }
        #endregion

        #region Edit
        [Fact]
        public void GivenId_WhenEditIsCalled_ThenReturnsNotFound()
        {
            //Arrange
            _service.Setup(service => service.GetById(_parkingZones[0].Id));

            //Act
            var edit = _parkingZoneController.Edit(_parkingZones[0].Id);

            //Assert    
            var notFoundResult = Assert.IsType<NotFoundResult>(edit);
            Assert.NotNull(edit);
            Assert.Equal(404, notFoundResult.StatusCode);
            _service.Verify(service => service.GetById(_parkingZones[0].Id), Times.Once);
        }

        [Fact]
        public void GivenId_WhenEditIsCalled_ThenReturnsEditViewModel()
        {
            //Arrange
            var expectedViewModel = new EditVM(_parkingZones[1]);
            _service
                .Setup(service => service.GetById(_parkingZones[1].Id))
                .Returns(_parkingZones[1]);

            //Act
            var edit = _parkingZoneController.Edit(_parkingZones[1].Id);
            var model = (edit as ViewResult).Model;

            //Assert
            Assert.IsType<ViewResult>(edit);
            Assert.IsType<EditVM>(model);
            Assert.Equal(JsonSerializer.Serialize(expectedViewModel), JsonSerializer.Serialize(model));
            _service.Verify(service => service.GetById(_parkingZones[1].Id), Times.Once);
        }

        [Fact]
        public void GivenIdAndModel_WhenEditIsCalled_ThenReturnsNotFoundResult()
        {
            //Arrange
            
            //Act
            var edit = _parkingZoneController.Edit(_parkingZones[0].Id, new EditVM());
                
            //Assert
            Assert.IsType<NotFoundResult>(edit);
        }

        [Fact]
        public void GivenIdAndModel_WhenEditIsCalled_ThenModelStateIsTrueAndItReturnsRedirectToAction()
        {
            //Arrange
            _service
                .Setup(_service => _service.GetById(_parkingZones[0].Id))
                .Returns(_parkingZones[0]);
                
            //Act
            var edit = _parkingZoneController.Edit(_parkingZones[0].Id, new EditVM(_parkingZones[0]));
            var model = edit as RedirectToActionResult;

            //Assert
            Assert.IsType<RedirectToActionResult>(edit);
            Assert.Equal("Index", model.ActionName);
            Assert.True(_parkingZoneController.ModelState.IsValid);
            _service.Verify(_service => _service.GetById(_parkingZones[0].Id), Times.Never);
        }

        [Fact]  
        public void GivenIdAndModel_WhenEditIsCalled_ThenModelStateIsFalseAndReturnsViewResult()
        {
            //Arrange
            _parkingZoneController.ModelState.AddModelError("Name", "Name is required");

            //Act
            var edit = _parkingZoneController.Edit(_parkingZones[0].Id, new EditVM() { Id = 1 });
            
            //Assert
            Assert.IsType<ViewResult>(edit);
            Assert.False(_parkingZoneController.ModelState.IsValid);
        }
        #endregion

        #region Delete
        [Fact]
        public void GivenId_WhenDeleteIsCalled_ThenReturnsNotFoundResult()
        {
            //Arrange
            _service.Setup(service => service.GetById(_parkingZones[0].Id));

            //Act
            var delete = _parkingZoneController.Delete(_parkingZones[0].Id);

            //Assert
            Assert.IsType<NotFoundResult>(delete);
            _service.Verify(service => service.GetById(_parkingZones[0].Id), Times.Once);
        }

        [Fact]
        public void GivenId_WhenDeleteIsCalled_ThenReturnsDeleteViewModel()
        {
            //Arrange
            _service
                .Setup(service => service.GetById(_parkingZones[1].Id))
                .Returns(_parkingZones[1]);

            //Act
            var delete = _parkingZoneController.Delete(_parkingZones[1].Id);
            var model = (delete as ViewResult).Model;

            //Assert
            Assert.IsType<ViewResult>(delete);
            Assert.IsType<ParkingZone>(model);
            Assert.Equal(JsonSerializer.Serialize(_parkingZones[1]), JsonSerializer.Serialize(model));
            Assert.NotNull(delete);
            Assert.NotNull(model);
            _service.Verify(service => service.GetById(_parkingZones[1].Id), Times.Once);
        }

        [Fact]
        public void GivenId_WhenDeleteConfirmedIsCalled_ThenReturnsRedirectToAction()
        {
            //Arrange
            _service
                .Setup(service => service.GetById(_parkingZones[0].Id))
                .Returns(_parkingZones[0]);

            _service.Setup(service => service.Delete(_parkingZones[0]));

            //Act
            var deleteConfirmed = _parkingZoneController.DeleteConfirmed(_parkingZones[0].Id);
            var model = deleteConfirmed as RedirectToActionResult;

            //Assert
            Assert.IsType<RedirectToActionResult>(deleteConfirmed);
            Assert.Equal("Index", model.ActionName);
            Assert.NotNull(deleteConfirmed);
            Assert.NotNull(model);
            _service.Verify(service => service.GetById(_parkingZones[0].Id), Times.Once);
            _service.Verify(service => service.Delete(_parkingZones[0]), Times.Once);
        }

        [Fact]
        public void GivenId_WhenDeleteConfirmedIsCalled_ThenReturnsNotFoundResult()
        {
            //Arrange
            _service.Setup(service => service.GetById(_parkingZones[0].Id));

            //Act
            var deleteConfirmed = _parkingZoneController.DeleteConfirmed(_parkingZones[0].Id);

            //Assert
            Assert.IsType<NotFoundResult>(deleteConfirmed);
            Assert.NotNull(deleteConfirmed);
            _service.Verify(service => service.GetById(_parkingZones[0].Id), Times.Once);
        }
        #endregion
    }
}
