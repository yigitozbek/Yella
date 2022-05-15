using Yella.Core.Domain.Dto;

namespace Yella.Core.Identity.Domain.Dtos
{
    public class ForgotPasswordDto : EntityDto<int>
    {
        public string Email { get; set; }
    }
}