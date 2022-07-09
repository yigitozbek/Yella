using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yella.Domain.Entities;

namespace Yella.Identity.Entities;

public class IdentityUser<TUser, TRole> : FullAuditedEntity<Guid>
    where TUser : IdentityUser<TUser, TRole>
    where TRole : IdentityRole<TUser, TRole>
{
    public IdentityUser()
    {
        
    }

    public string UserName { get; set; }

    [Required, MinLength(5)]
    public string Email { get; set; }

    public byte[] PasswordSalt { get; set; }
    public byte[] PasswordHash { get; set; }


    [Required, MinLength(5), MaxLength(50)]
    public string Name { get; set; }

    [Required, MinLength(5), MaxLength(50)]
    public string Surname { get; set; }

    public DateTime? LastLogin { get; set; }
    public DateTime? Joined { get; set; }

    public short IncorrectPasswordAttempts { get; set; }
    public bool IsBlocked { get; set; }
    public DateTime? BlockedDate { get; set; }

    public Guid? BlockedUserId { get; set; }

    [ForeignKey(nameof(BlockedUserId))]
    public virtual TUser BlockedUser { get; set; }

    public virtual ICollection<UserRole<TUser, TRole>> UserRoles { get; set; }

}