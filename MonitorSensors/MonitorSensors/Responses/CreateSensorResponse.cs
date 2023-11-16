namespace MonitorSensors.Responses;

public class CreateSensorResponse
{
    public CreateSensorResult Result { get; set; }
}
public enum CreateSensorResult
{
    ServerError,
    Success,
}