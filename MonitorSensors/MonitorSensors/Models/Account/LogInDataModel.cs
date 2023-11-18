using System.ComponentModel.DataAnnotations;

namespace MonitorSensors.Models.Account;

public class LogInDataModel
{
    [Required]
    public string Login { get; set; }
    [Required]
    public string Password { get; set; }
}