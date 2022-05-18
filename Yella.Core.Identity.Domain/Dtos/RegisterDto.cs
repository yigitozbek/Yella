using Microsoft.AspNetCore.Http;
using Yella.Core.Domain.Dto;

namespace Yella.Core.Identity.Domain.Dtos;

public class RegisterDto : EntityDto<int>
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public IFormFile? Image { get; set; }
}