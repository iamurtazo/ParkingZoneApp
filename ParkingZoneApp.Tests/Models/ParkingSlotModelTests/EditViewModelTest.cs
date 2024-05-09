using ParkingZoneApp.Enums;
using ParkingZoneApp.ViewModels.ParkingSlotViewModels;
using System.ComponentModel.DataAnnotations;

namespace ParkingZoneApp.Tests.Models.ParkingSlotModelTests;

public class EditViewModelTest
{
    public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                new object[] { 1, true, SlotCategory.Premium, true },
                new object[] { 22, false, SlotCategory.Premium, true }
            };

    [Theory]
    [MemberData(nameof(Data))]
    public void GivenItemsTobeValidated_WhenCreatingEditViewModel_ThenValidationIsPerformed
        (int number, bool isAvailableForBooking, SlotCategory category, bool expected)
    {
        //Arrange
        var editModel = new EditViewModel()
        {
            Number = number,
            IsAvailableForBooking = isAvailableForBooking,
            Category = category
        };

        var validationContext = new ValidationContext(editModel, null, null);
        var validationResult = new List<ValidationResult>();

        //Act
        var result = Validator.TryValidateObject(editModel, validationContext, validationResult);

        //Assert
        Assert.Equal(expected, result);
    }
}
