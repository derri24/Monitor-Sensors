using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonitorSensors.Entities;

public class Uint
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
}