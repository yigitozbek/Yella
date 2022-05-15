using System.ComponentModel.DataAnnotations;
using Yella.Core.Domain.Dto;

namespace Yella.Core.Identity.Domain.Dtos
{
    public class UserRoleAddDto : EntityDto
    {
        [Required] public Guid UserId { get; set; }
        [Required] public List<Guid> RoleIds { get; set; }
    }
}