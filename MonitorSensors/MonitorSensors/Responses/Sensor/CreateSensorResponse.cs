namespace MonitorSensors.Responses.Sensor;

public class CreateSensorResponse
{
    public CreateSensorResult Result { get; set; }
}
public enum CreateSensorResult
{
    ServerError,
    Success,
}