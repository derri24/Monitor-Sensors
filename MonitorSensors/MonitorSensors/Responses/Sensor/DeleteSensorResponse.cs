namespace MonitorSensors.Responses.Sensor;

public class DeleteSensorResponse
{
    public DeleteSensorResult Result { get; set; }
}

public enum DeleteSensorResult
{
    ServerError,
    NotFound,
    Success,
}