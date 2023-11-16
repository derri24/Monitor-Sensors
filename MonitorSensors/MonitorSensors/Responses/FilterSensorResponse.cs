using MonitorSensors.Models;

namespace MonitorSensors.Responses;

public class FilterSensorResponse
{
    public List<SensorModel> Items { get; set; }
    public FilterSensorResult Result { get; set; }
}


public class SensorModel
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
public enum FilterSensorResult
{
    ServerError,
    Success,
}