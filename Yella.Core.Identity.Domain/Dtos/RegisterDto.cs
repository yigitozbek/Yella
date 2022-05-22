using Microsoft.AspNetCore.Http;
using Yella.Core.Domain.Dto;

namespace Yella.Core.Identity.Dtos;

public class RegisterDto : EntityDto
{
    protected RegisterDto(string userName, string password, string email, string name, string surname)
    {
        UserName = userName;
        Password = password;
        Email = email;
        Name = name;
        Surname = surname;
    }

    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public List<Guid> RoleIds { get; set; }

}