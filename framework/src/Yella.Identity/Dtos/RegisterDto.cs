using Microsoft.AspNetCore.Http;
 
namespace Yella.Identity.Dtos;

public class RegisterDto  
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public List<Guid> RoleIds { get; set; }

}