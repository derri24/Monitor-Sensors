namespace MonitorSensors.Responses.Account;

public class SignUpResponse
{

    public SingUpResult Result { get; set; }
}

public enum SingUpResult
{
    UserAlreadyExist,
    ClientError,
    ServerError,
    Success
}