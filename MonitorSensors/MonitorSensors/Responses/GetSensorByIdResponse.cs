namespace MonitorSensors.Responses;

public class GetSensorByIdResponse
{
    public SensorModel Item { get; set; }
    public GetSensorByIdResult Result { get; set; }
}

public enum GetSensorByIdResult
{
    ServerError,
    Success,
}