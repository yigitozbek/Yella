using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yella.Core.Domain.Entities;

namespace Yella.Core.Identity.Entities;

public class IdentityUser<TUser, TRole> : FullAuditedEntity<Guid>
    where TUser : IdentityUser<TUser, TRole>
    where TRole : IdentityRole<TUser, TRole>
{
    public IdentityUser(Guid id, string userName, string email, byte[] passwordSalt, byte[] passwordHash, string name, string surname, byte[] image, DateTime? lastLogin, DateTime? joined, short incorrectPasswordAttempts, bool isBlocked, DateTime? blockedDate, Guid? blockedUserId, TUser blockedUser, ICollection<UserRole<TUser, TRole>> userRoles) : base(id)
    {
        UserName = userName;
        Email = email;
        PasswordSalt = passwordSalt;
        PasswordHash = passwordHash;
        Name = name;
        Surname = surname;
        Image = image;
        LastLogin = lastLogin;
        Joined = joined;
        IncorrectPasswordAttempts = incorrectPasswordAttempts;
        IsBlocked = isBlocked;
        BlockedDate = blockedDate;
        BlockedUserId = blockedUserId;
        BlockedUser = blockedUser;
        UserRoles = userRoles;
    }

    public IdentityUser(string userName, string email, byte[] passwordSalt, byte[] passwordHash, string name, string surname, byte[] image, DateTime? lastLogin, DateTime? joined, short incorrectPasswordAttempts, bool isBlocked, DateTime? blockedDate, Guid? blockedUserId, TUser blockedUser, ICollection<UserRole<TUser, TRole>> userRoles)
    {
        UserName = userName;
        Email = email;
        PasswordSalt = passwordSalt;
        PasswordHash = passwordHash;
        Name = name;
        Surname = surname;
        Image = image;
        LastLogin = lastLogin;
        Joined = joined;
        IncorrectPasswordAttempts = incorrectPasswordAttempts;
        IsBlocked = isBlocked;
        BlockedDate = blockedDate;
        BlockedUserId = blockedUserId;
        BlockedUser = blockedUser;
        UserRoles = userRoles;
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

    public byte[] Image { get; set; }
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