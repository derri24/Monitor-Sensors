using MonitorSensors.Models.Account;
using MonitorSensors.Responses.Account;

namespace MonitorSensors.Services.Interfaces;

public interface IAccountService
{
    public Task<SignUpResponse> Registration(SingUpDataModel model);
    public Task<LogInResponse> Authorization(LogInDataModel model);

}