
namespace Yella.Identity.Models;

public class ResetPasswordModel  
{
    public Guid Id { get; set; }

    public string Username { get; set; }

    public string CurrentPassword { get; set; }

    public string ConfirmPassword { get; set; }

    public string NewPassword { get; set; }
}