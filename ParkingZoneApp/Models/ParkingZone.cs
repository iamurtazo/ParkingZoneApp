using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingZoneApp.Models;

[Table("ParkingZones")]
public class ParkingZone
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public DateTime EstablishmentDate { get; set; }
    public virtual ICollection<ParkingSlot> ParkingSlots { get; set; }
}
