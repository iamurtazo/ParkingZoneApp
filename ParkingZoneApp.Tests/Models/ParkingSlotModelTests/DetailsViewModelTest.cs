using ParkingZoneApp.Enums;
using ParkingZoneApp.ViewModels.ParkingSlotVMs;
using System.ComponentModel.DataAnnotations;

namespace ParkingZoneApp.Tests.Models.ParkingSlotModelTests;

public class DetailsViewModelTest
{
    public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[] { 5, true, SlotCategory.Premium, true },
            new object[] { 6, false, SlotCategory.Standard, true },
        };

    [Theory]
    [MemberData(nameof(Data))]
    public void GivenItemsToBeValidated_WhenCreatingDetailsModel_ThenValidationIsPerformed
        (int number, bool isAvailableForBooking, SlotCategory category, bool expected)
    {
        //Arrange   
        var detailsVM = new DetailsVM()
        {
            Number = number,
            IsAvailableForBooking = isAvailableForBooking,
            Category = category
        };

        var validationContext = new ValidationContext(detailsVM, null, null);
        var validationResult = new List<ValidationResult>();

        //Act
        var result = Validator.TryValidateObject(detailsVM, validationContext, validationResult);

        //Assert
        Assert.Equal(expected, result);
    }
}

