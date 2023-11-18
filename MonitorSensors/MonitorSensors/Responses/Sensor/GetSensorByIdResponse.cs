using MonitorSensors.Models;

namespace MonitorSensors.Responses;

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