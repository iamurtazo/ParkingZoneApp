using ParkingZoneApp.Enums;
using ParkingZoneApp.ViewModels.ParkingSlotViewModels;
using System.ComponentModel.DataAnnotations;
namespace ParkingSlots.Tests.Models;
public class CreateViewModelTest
{
    public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[] { 1, true, SlotCategory.VIP, true },
            new object[] { 2, false, SlotCategory.Premium, true},
            new object[] { 3, true, SlotCategory.Standard, true }
        };

    [Theory]
    [MemberData(nameof(Data))]
    public void GivenItemsToBeValidated_WhenCreatingViewModel_ThenValidationIsPerformed(
        int number, bool isAvailableForBooking, SlotCategory category, bool expected)
    {
        // Arrange
        CreateViewModel createViewModel = new()
        {
            Number = number,
            IsAvailableForBooking = isAvailableForBooking,
            Category = category
        };
        var validationContext = new ValidationContext(createViewModel, null, null);
        var validationResult = new List<ValidationResult>();

        // Act
        var result = Validator.TryValidateObject(createViewModel, validationContext, validationResult);

        // Assert
        Assert.Equal(expected, result);
    }
}

