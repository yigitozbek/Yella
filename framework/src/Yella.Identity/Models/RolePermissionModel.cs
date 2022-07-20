namespace Yella.Identity.Models;

public class RolePermissionModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Code { get; set; }
    public PermissionModel[] Permissions { get; set; }
}