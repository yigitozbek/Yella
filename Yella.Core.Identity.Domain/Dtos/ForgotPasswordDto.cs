using Archseptia.Core.Domain.Dto;

namespace Yella.Core.IdentityService.Dtos
{
    public class ForgotPasswordDto : EntityDto<int>
    {
        public string Email { get; set; }
    }
}