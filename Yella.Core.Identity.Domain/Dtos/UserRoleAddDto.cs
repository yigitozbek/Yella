using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Archseptia.Core.Domain.Dto;

namespace Archseptia.Core.Identity.Service.Dtos
{
    public class UserRoleAddDto : EntityDto
    {
        [Required] public Guid UserId { get; set; }
        [Required] public List<Guid> RoleIds { get; set; }
    }
}