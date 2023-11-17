namespace MonitorSensors.Models;

public class FilterSensorModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Model { get; set; }
    public RangeModel Range { get; set; }
    public string Type { get; set; }
    public string Location { get; set; }
}