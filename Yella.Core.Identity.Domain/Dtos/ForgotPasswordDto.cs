using Yella.Core.Domain.Dto;

namespace Yella.Core.Identity.Dtos;

public class ForgotPasswordDto : EntityDto<int>
{
    protected ForgotPasswordDto(string email)
    {
        Email = email;
    }

    protected ForgotPasswordDto(int id, string email) : base(id)
    {
        Email = email;
    }

    public string Email { get; set; }
}