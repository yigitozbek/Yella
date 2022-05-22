﻿using System.ComponentModel.DataAnnotations;
using Yella.Core.Domain.Dto;

namespace Yella.Core.Identity.Dtos;

public class UserRoleAddDto : EntityDto
{
    public UserRoleAddDto(Guid userId, List<Guid> roleIds)
    {
        UserId = userId;
        RoleIds = roleIds;
    }

    public Guid UserId { get; set; }
    public List<Guid> RoleIds { get; set; }
}