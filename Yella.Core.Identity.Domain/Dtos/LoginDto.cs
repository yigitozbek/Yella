using Yella.Core.Domain.Dto;

namespace Yella.Core.Identity.Domain.Dtos
{
    public class LoginDto : EntityDto<Guid>
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsRemember { get; set; }
    }
}