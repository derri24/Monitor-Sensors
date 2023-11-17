using MonitorSensors.Models;

namespace MonitorSensors.Responses;

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