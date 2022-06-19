using System.ComponentModel.DataAnnotations;
 
namespace Yella.Framework.Identity.Dtos;

public class UserRoleRemoveDto  
{
    public Guid UserId { get; set; }
    public List<Guid> RoleIds { get; set; }
}