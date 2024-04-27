using ParkingZoneApp.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace ParkingZoneApp.Tests.Models
{
    public class CreateViewModelTest
    {
        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                new object[] { null, "Address 1", false },
                new object[] { "Parking Zone 1", null, false },
                new object[] { "Parking Zone 1", "Address 1", true }
            };

        [Theory]
        [MemberData(nameof(Data))]
        public void GivenItemsToBeValidated_WhenCreatingViewModels_ThenValidationIsPerformed
            (string name, string address, bool expected)
        {
            //Arrange   
            var createVM = new CreateVM()
            {
                Name = name,
                Address = address
            };

            var validationContext = new ValidationContext(createVM, null, null);
            var validationResult = new List<ValidationResult>();

            //Act
            var result = Validator.TryValidateObject(createVM, validationContext, validationResult);

            //Assert
            Assert.Equal(expected, result);
        }
    }
}
