using Yella.Core.Domain.Dto;

namespace Yella.Core.Identity.Domain.Dtos
{
    public class ResetPasswordDto : EntityDto<Guid>
    {
        public string Username { get; set; }

        public string CurrentPassword { get; set; }


        public string ConfirmPassword { get; set; }

        public string NewPassword { get; set; }


    }

}