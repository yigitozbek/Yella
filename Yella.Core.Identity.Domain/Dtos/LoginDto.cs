using Yella.Core.Domain.Dto;

namespace Yella.Core.Identity.Dtos;

public class LoginDto : EntityDto<Guid>
{
    protected LoginDto(string email, string username, string password, bool isRemember)
    {
        Email = email;
        Username = username;
        Password = password;
        IsRemember = isRemember;
    }

    protected LoginDto(Guid id, string email, string username, string password, bool isRemember) : base(id)
    {
        Email = email;
        Username = username;
        Password = password;
        IsRemember = isRemember;
    }

    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public bool IsRemember { get; set; }
}