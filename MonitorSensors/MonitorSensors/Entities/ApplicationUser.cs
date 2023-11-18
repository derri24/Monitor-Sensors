using Microsoft.AspNetCore.Identity;

namespace MonitorSensors.Entities;

public class ApplicationUser: IdentityUser
{
    public string Name { get; set; }
}