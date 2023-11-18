using System.ComponentModel.DataAnnotations;

namespace MonitorSensors.Models.Account;

public class SingUpDataModel
{

    public string Name { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    
    public string Role { get; set; }
    
}