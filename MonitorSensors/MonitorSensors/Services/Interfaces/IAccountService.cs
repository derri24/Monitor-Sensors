using MonitorSensors.Models.Account;
using MonitorSensors.Responses.Account;

namespace MonitorSensors.Services.Interfaces;

public interface IAccountService
{
    public Task<RegistrationResponse> Registration(RegistrationDataModel model);
    public Task<AuthenticationResponse> Authentication(AuthenticationDataModel model);

}