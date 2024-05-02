using ParkingZoneApp.Enums;
using ParkingZoneApp.ViewModels.ParkingSlotViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSlots.Tests.Models;

public class ListOfParkingSlotsVMTest
{
    public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[] { 1, 34, false, SlotCategory.VIP, true },
            new object[] { 2, 35, true, SlotCategory.Premium, true},
            new object[] { 1, 67, false, SlotCategory.Standard, true }
        };

    [Theory]
    [MemberData(nameof(Data))]
    public void GivenItemsToBeValidated_WhenListOfItemsViewModel_ThenValidationIsPerformed
        (int parkingZoneId, int number, bool isAvailableForBooking, SlotCategory category, bool expected)
    {
        // Arrange
        var createViewModel = new CreateViewModel
        {
            ParkingZoneId = parkingZoneId,
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
