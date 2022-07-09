using System.ComponentModel.DataAnnotations;
 
namespace Yella.Identity.Dtos;

public class UserRoleRemoveDto  
{
    public Guid UserId { get; set; }
    public List<Guid> RoleIds { get; set; }
}