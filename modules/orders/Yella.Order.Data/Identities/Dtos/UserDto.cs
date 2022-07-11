using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yella.Domain.Dto;

namespace Yella.Order.Data.Identities.Dtos;

public class UserDto : FullAuditedEntityDto<Guid>
{
    public string UserName { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public short IncorrectPasswordAttempts { get; set; }
    public bool IsBlocked { get; set; }
    public DateTime? BlockedDate { get; set; }

    public Guid? BlockedUserId { get; set; }
    public virtual UserDto BlockedUser { get; set; }


    public string Image { get; set; }
    public DateTime? LastLogin { get; set; }
    public DateTime? Joined { get; set; }


}