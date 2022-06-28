
namespace Yella.Framework.Identity.Dtos;

public class ResetPasswordDto  
{
    public Guid Id { get; set; }

    public string Username { get; set; }

    public string CurrentPassword { get; set; }

    public string ConfirmPassword { get; set; }

    public string NewPassword { get; set; }
}