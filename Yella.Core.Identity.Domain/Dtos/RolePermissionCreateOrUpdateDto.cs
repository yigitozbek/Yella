using System;
using System.ComponentModel.DataAnnotations;
using Archseptia.Core.Domain.Dto;

namespace Archseptia.Core.Identity.Service.Dtos
{
    public class RolePermissionCreateOrUpdateDto : EntityDto<Guid>
    {
        [Required, MinLength(5), MaxLength(50)]
        public string RoleName { get; set; }

        [Required]
        public string Description { get; set; }
        public PermissionDto[] Permissions { get; set; }
    }
}