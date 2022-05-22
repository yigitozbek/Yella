using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using Yella.Core.Domain.Entities;

namespace Yella.Core.Identity.Entities;

public class UserLoginLog<TUser, TRole> : Entity<long>
    where TUser : IdentityUser<TUser, TRole>
    where TRole : IdentityRole<TUser, TRole>
{
    protected UserLoginLog(Guid userId, TUser user, string userAgent, DateTime loginDate, IPAddress ipAddress, bool isSuccessful)
    {
        UserId = userId;
        User = user;
        UserAgent = userAgent;
        LoginDate = loginDate;
        IpAddress = ipAddress;
        IsSuccessful = isSuccessful;
    }

    protected UserLoginLog(long id, Guid userId, TUser user, string userAgent, DateTime loginDate, IPAddress ipAddress, bool isSuccessful) : base(id)
    {
        UserId = userId;
        User = user;
        UserAgent = userAgent;
        LoginDate = loginDate;
        IpAddress = ipAddress;
        IsSuccessful = isSuccessful;
    }

    public Guid UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public virtual TUser User { get; set; }
         
    public string UserAgent { get; set; }

    public DateTime LoginDate { get; set; }

    public IPAddress IpAddress { get; set; }

    public bool IsSuccessful { get; set; }
}