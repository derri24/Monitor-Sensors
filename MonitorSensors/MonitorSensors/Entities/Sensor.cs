using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonitorSensors.Entities;

public class Sensor
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Model { get; set; }
    
    public int RangeFrom { get; set; }
    public int RangeTo { get; set; }
    public string Location { get; set; }
    public string Description { get; set; }
    
    public int TypeId { get; set; }
    public virtual Type Type { get; set; }
    
    public int UnitId { get; set; }
    public Unit Unit { get; set; }
  
}