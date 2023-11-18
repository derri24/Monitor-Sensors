using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MonitorSensors.Models.Account;
using MonitorSensors.Responses.Account;
using MonitorSensors.Services.Interfaces;
using AuthenticationProperties = Microsoft.AspNetCore.Authentication.AuthenticationProperties;

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
    public async Task<IActionResult> Authorization()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Authorization([FromBody] LogInDataModel model)
    {
        var response = await _accountService.Authorization(model);
        if (response.Token != null)
            HttpContext.Response.Cookies.Append("token", response.Token);
        return Ok();
    }

    [HttpPost]
    public async Task<SignUpResponse> Registration([FromBody] SingUpDataModel model)
    {
        return await _accountService.Registration(model);
    }
}