using Moq;
using ParkingZoneApp.Models;
using ParkingZoneApp.Repositories.ParkingZoneRepository.ParkingZoneRepository;
using ParkingZoneApp.Services;
using ParkingZoneApp.Services.ParkingZoneService;
using System.Text.Json;

namespace ParkingZoneApp.Tests.Services
{
    public class ParkingZoneServicesTest
    {
        private readonly Mock<IParkingZoneRepository> _parkingZoneRepository;
        private readonly IParkingZoneService _service;
        private readonly ParkingZone _parkingZone;
        private readonly int _id = 1;

        #region Cosntructor
        public ParkingZoneServicesTest()
        {
            _parkingZoneRepository = new Mock<IParkingZoneRepository>();
            _service = new ParkingZoneService(_parkingZoneRepository.Object);
            _parkingZone = new ParkingZone
            {
                Id = 1,
                Name = "Parking Zone 1",
                Address = "Address 1",
                EstablishmentDate = DateTime.Now
            };

        }
        #endregion

        #region Insert
        [Fact]
        public void GivenModel_WhenInsertIsCalled_ThenModelIsAdded()
        {
            //Arrange
            _parkingZoneRepository.Setup(x => x.Insert(_parkingZone));

            //Act
            _service.Insert(_parkingZone);

            //Assert
            _parkingZoneRepository.Verify(x => x.Insert(_parkingZone), Times.Once);
        }
        #endregion

        #region Update
        [Fact]
        public void GivenModel_WhenUpdateIsCalled_ThenModelIsUpdated()
        {
            //Arrange
            _parkingZoneRepository.Setup(x => x.Update(_parkingZone));

            //Act
            _service.Update(_parkingZone);

            //Assert
            _parkingZoneRepository.Verify(x => x.Update(_parkingZone), Times.Once);
            
        }
        #endregion

        #region Delete
        [Fact] public void GivenModel_WhenDeleteIsCalled_ThenModelIsDeleted()
        {
            //Arrange
            _parkingZoneRepository.Setup(x => x.Delete(_parkingZone));

            //Act
            _service.Delete(_parkingZone);

            //Assert
            _parkingZoneRepository.Verify(x => x.Delete(_parkingZone), Times.Once);

        }
        #endregion

        #region GetById
        [Fact]
        public void GivenId_WhenGetByIdIsCalled_ThenModelIsReturned()
        {
            //Arrange
            _parkingZoneRepository.Setup(x => x.GetById(_id)).Returns(_parkingZone);

            //Act
            var result = _service.GetById(_id);

            //Assert
            Assert.IsType<ParkingZone>(result);
            Assert.Equal(JsonSerializer.Serialize(_parkingZone), JsonSerializer.Serialize(result));
            Assert.NotNull(result);
            _parkingZoneRepository.Verify(x => x.GetById(_id), Times.Once);
        }
        #endregion

        #region GetAll
        [Fact]
        public void GivenNothing_WhenGetAllIsCalled_ThenModelsAreReturned()
        {
            //Arrange
            var parkingZones = new List<ParkingZone>() { _parkingZone };
            _parkingZoneRepository.Setup(x => x.GetAll()).Returns(parkingZones);

            //Act
            var getAll = _service.GetAll();

            //Assert
            Assert.Equal(JsonSerializer.Serialize(getAll), JsonSerializer.Serialize(parkingZones));
            Assert.IsAssignableFrom<IEnumerable<ParkingZone>>(parkingZones);
            Assert.NotNull(getAll);
            _parkingZoneRepository.Verify(x => x.GetAll(), Times.Once);
        }
        #endregion
    }
}

