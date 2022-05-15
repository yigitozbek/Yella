using System;
using System.ComponentModel.DataAnnotations;
using Archseptia.Core.Domain.Dto;
using Microsoft.AspNetCore.Http;

namespace Archseptia.Core.Identity.Service.Dtos
{
    public class RegisterDto : EntityDto<int>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public IFormFile? Image { get; set; }
    }
}