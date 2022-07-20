namespace Yella.Identity.Models;

public class UserRoleRemoveModel  
{
    public Guid UserId { get; set; }
    public List<Guid> RoleIds { get; set; }
}