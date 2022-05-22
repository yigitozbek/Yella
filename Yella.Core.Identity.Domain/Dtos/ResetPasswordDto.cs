using Yella.Core.Domain.Dto;

namespace Yella.Core.Identity.Dtos;

public class ResetPasswordDto : EntityDto<Guid>
{
    protected ResetPasswordDto(string username, string currentPassword, string confirmPassword, string newPassword)
    {
        Username = username;
        CurrentPassword = currentPassword;
        ConfirmPassword = confirmPassword;
        NewPassword = newPassword;
    }

    protected ResetPasswordDto(Guid id, string username, string currentPassword, string confirmPassword, string newPassword) : base(id)
    {
        Username = username;
        CurrentPassword = currentPassword;
        ConfirmPassword = confirmPassword;
        NewPassword = newPassword;
    }

    public string Username { get; set; }

    public string CurrentPassword { get; set; }


    public string ConfirmPassword { get; set; }

    public string NewPassword { get; set; }


}