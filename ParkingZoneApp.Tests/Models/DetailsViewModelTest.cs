using ParkingZoneApp.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace ParkingZoneApp.Tests.Models
{
    public class DetailsViewModelTest
    {
        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                new object[] { 1, null, "Address 1", DateTime.Now, false },
                new object[] { 2, "Parking Zone 2", null, DateTime.Now, false },
                new object[] { 3, "Parking Zone 3", "Address 3", DateTime.Now, true }
            };

        [Theory]
        [MemberData(nameof(Data))]
        public void GivenItemsToBeValidated_WhenCreatingDetailsModel_ThenValidationIsPerformed
            (int id, string name, string address, DateTime date, bool expected)
        {
            //Arrange   
            var detailsVM = new DetailsVM()
            {
                Id = id,
                Name = name,
                Address = address,
                EstablishmentDate = date
            };

            var validationContext = new ValidationContext(detailsVM, null, null);
            var validationResult = new List<ValidationResult>();

            //Act
            var result = Validator.TryValidateObject(detailsVM, validationContext, validationResult);

            //Assert
            Assert.Equal(expected, result);
        }
    }
}
