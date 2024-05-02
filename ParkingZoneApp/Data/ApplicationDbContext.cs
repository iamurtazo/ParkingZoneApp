using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ParkingZoneApp.Models;

namespace ParkingZoneApp.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }
    public DbSet<ParkingZone> ParkingZones { get; set; }
    public DbSet<ParkingSlot> ParkingSlots { get; set; }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<ParkingSlot>().HasData
    //    (
    //        new ParkingSlot { Id = 1, Number = 55, Category = Enums.SlotCategory.VIP, IsAvailableForBooking = true },
    //        new ParkingSlot { Id = 2, Number = 56, Category=Enums.SlotCategory.Standard, IsAvailableForBooking = true },
    //        new ParkingSlot { Id = 3, Number = 57, Category=Enums.SlotCategory.Premium, IsAvailableForBooking = false}
    //    );
    //}
}
