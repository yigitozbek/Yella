using Yella.Core.Domain.Dto;
using Yella.Core.Identity.Domain.Entities;

namespace Yella.Core.Identity.Domain.Dtos;

public class AuthenticateDto<TUser, TRole> : EntityDto<Guid>
    where TUser : IdentityUser<TUser, TRole>
    where TRole : IdentityRole<TUser, TRole>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Username { get; set; }
    public string JwtToken { get; set; }


    public AuthenticateDto(TUser user, string jwtToken)
    {
        Id = user.Id;
        Name = user.Name;
        Surname = user.Surname;
        Username = user.UserName;
        JwtToken = jwtToken;
    }
}