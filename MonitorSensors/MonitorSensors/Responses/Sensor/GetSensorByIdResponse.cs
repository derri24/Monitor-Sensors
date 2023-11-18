using MonitorSensors.Models.Sensor;

namespace MonitorSensors.Responses.Sensor;

public class GetSensorByIdResponse
{
    public GetByIdSensorModel Item { get; set; }
    public GetSensorByIdResult Result { get; set; }
}

public enum GetSensorByIdResult
{
    ServerError,
    Success,
}