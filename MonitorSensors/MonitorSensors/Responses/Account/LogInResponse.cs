using System.Security.Claims;

namespace MonitorSensors.Responses.Account;

public class LogInResponse
{
    //public ClaimsIdentity Claims { get; set; }
    public string? Token { get; set; }
    public LoginResult Result { get; set; }
}

public enum LoginResult
{
    IncorrectLoginOrPassword,
    ServerError,
    Success,
}