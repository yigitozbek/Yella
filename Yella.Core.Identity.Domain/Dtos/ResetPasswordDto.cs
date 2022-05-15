using System;
using System.ComponentModel.DataAnnotations;
using Archseptia.Core.Domain.Dto;

namespace Archseptia.Core.Identity.Service.Dtos
{
    public class ResetPasswordDto : EntityDto<Guid>
    {
        public string Username { get; set; }

        public string CurrentPassword { get; set; }


        public string ConfirmPassword { get; set; }

        public string NewPassword { get; set; }


    }

}