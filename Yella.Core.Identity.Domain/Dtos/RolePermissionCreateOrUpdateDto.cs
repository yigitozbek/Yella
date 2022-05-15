using System.ComponentModel.DataAnnotations;
using Yella.Core.Domain.Dto;

namespace Yella.Core.Identity.Domain.Dtos
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