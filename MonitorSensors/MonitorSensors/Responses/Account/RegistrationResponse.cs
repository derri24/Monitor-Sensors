namespace MonitorSensors.Responses.Account;

public class RegistrationResponse
{
    public RegistrationResult Result { get; set; }
}

public enum RegistrationResult
{
    UserAlreadyExists,
    ClientError,
    ServerError,
    Success
}