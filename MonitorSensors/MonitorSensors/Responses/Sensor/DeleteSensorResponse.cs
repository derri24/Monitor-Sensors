namespace MonitorSensors.Responses;

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