using MonitorSensors.Models.Sensor;

namespace MonitorSensors.Responses.Sensor;

public class FilterSensorResponse
{
    public List<FilterSensorModel> Items { get; set; }
    public FilterSensorResult Result { get; set; }
}

public enum FilterSensorResult
{
    ServerError,
    Success,
}