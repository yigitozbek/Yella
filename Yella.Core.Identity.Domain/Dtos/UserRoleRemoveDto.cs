using System.ComponentModel.DataAnnotations;
using Yella.Core.Domain.Dto;

namespace Yella.Core.Identity.Dtos;

public class UserRoleRemoveDto : EntityDto
{
    public UserRoleRemoveDto(Guid userId, List<Guid> roleIds)
    {
        UserId = userId;
        RoleIds = roleIds;
    }

    public Guid UserId { get; set; }
    public List<Guid> RoleIds { get; set; }
}