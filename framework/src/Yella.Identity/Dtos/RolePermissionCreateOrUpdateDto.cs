using System.ComponentModel.DataAnnotations;
 
namespace Yella.Identity.Dtos;

public class RolePermissionCreateOrUpdateDto  
{
    public Guid Id { get; set; }
    public string RoleName { get; set; }

    public string Description { get; set; }

    public PermissionDto[] Permissions { get; set; }
}