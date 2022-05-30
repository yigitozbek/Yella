using System.ComponentModel.DataAnnotations;

namespace Yella.Core.Identity.Dtos;

public class UserRoleAddDto
{
    public Guid UserId { get; set; }
    public List<Guid> RoleIds { get; set; }
}