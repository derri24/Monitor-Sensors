namespace MonitorSensors.Models.Sensor;

public class GetByIdSensorModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Model { get; set; }
    public RangeModel Range { get; set; }
    public int TypeId { get; set; }
    public int UnitId { get; set; }
    public string Location { get; set; }
    public string Description { get; set; }
}