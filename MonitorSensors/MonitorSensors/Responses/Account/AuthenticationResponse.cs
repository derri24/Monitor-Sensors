using System.Security.Claims;

namespace MonitorSensors.Responses.Account;

public class AuthenticationResponse
{
    public string? Token { get; set; }
    public AuthenticationResult Result { get; set; }
}

public enum AuthenticationResult
{
    IncorrectLoginOrPassword,
    ServerError,
    Success,
}