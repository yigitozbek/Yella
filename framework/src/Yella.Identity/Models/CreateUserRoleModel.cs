namespace Yella.Identity.Models;

public class CreateUserRoleModel
{
    public Guid UserId { get; set; }
    public List<Guid> RoleIds { get; set; }
}