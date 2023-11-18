using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MonitorSensors.Data;
using MonitorSensors.Models.Account;
using MonitorSensors.Responses.Account;
using MonitorSensors.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using MonitorSensors.Entities;

namespace MonitorSensors.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly IConfiguration _configuration;

    public AccountService(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration)
    {
        _configuration = configuration;
        this.userManager = userManager;
        this.roleManager = roleManager;
    }

    public async Task<RegistrationResponse> Registration(RegistrationDataModel model)
    {
        var response = new RegistrationResponse();

        var userExists = await userManager.FindByNameAsync(model.Name);
        if (userExists != null)
        {
            response.Result = RegistrationResult.UserAlreadyExists;
            return response;
        }

        ApplicationUser user = new()
        {
            SecurityStamp = Guid.NewGuid().ToString(),
            Name = model.Name,
            UserName = model.Login,
        };
        user.Id = Guid.NewGuid().ToString();
        var createUserResult = await userManager.CreateAsync(user, model.Password);
        if (!createUserResult.Succeeded)
        {
            response.Result = RegistrationResult.ClientError;
            return response;
        }

        if (!await roleManager.RoleExistsAsync(model.Role))
            await roleManager.CreateAsync(new IdentityRole(model.Role));

        if (await roleManager.RoleExistsAsync(model.Role))
            await userManager.AddToRoleAsync(user, model.Role);

        response.Result = RegistrationResult.Success;
        return response;
    }
    
    public async Task<AuthenticationResponse> Authentication(AuthenticationDataModel model)
    {
        var response = new AuthenticationResponse();
        var user = await userManager.FindByNameAsync(model.Login);
        if (user == null || !await userManager.CheckPasswordAsync(user, model.Password))
        {
            response.Result = AuthenticationResult.IncorrectLoginOrPassword;
            return response;
        }

        var userRoles = await userManager.GetRolesAsync(user);
        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        
        foreach (var userRole in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }
        
        string token = GenerateToken(authClaims);
        response.Token = token;
    
        response.Result = AuthenticationResult.Success;
        return response;
    }
    
    private string GenerateToken(IEnumerable<Claim> claims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTKey:Secret"]));
        var tokenExpiryTimeInHour = Convert.ToInt64(_configuration["JWTKey:TokenExpiryTimeInHour"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddHours(tokenExpiryTimeInHour),
            SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
            Subject = new ClaimsIdentity(claims)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}