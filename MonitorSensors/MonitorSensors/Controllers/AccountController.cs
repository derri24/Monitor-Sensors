using Microsoft.AspNetCore.Mvc;
using MonitorSensors.Models.Account;
using MonitorSensors.Responses.Account;
using MonitorSensors.Services.Interfaces;

namespace MonitorSensors.Controllers;

[Route("[controller]/[action]")]
public class AccountController : Controller
{
    private IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    public async Task<IActionResult> Registration()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Authentication()
    {
        return View();
    }

    [HttpPost]
    public async Task<AuthenticationResponse> Authentication([FromBody] AuthenticationDataModel model)
    {
        var response = await _accountService.Authentication(model);
        if (response.Token != null)
        {
            HttpContext.Response.Cookies.Append("token", response.Token);
        }
        return response;
    }

    [HttpPost]
    public async Task<RegistrationResponse> Registration([FromBody] RegistrationDataModel model)
    {
        return await _accountService.Registration(model);
    }
}