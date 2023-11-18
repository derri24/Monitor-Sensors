namespace MonitorSensors.Responses.Sensor;

public class UpdateSensorResponse
{
    public UpdateSensorResult Result { get; set; }
}

public enum UpdateSensorResult
{
    ServerError,
    Success,
}